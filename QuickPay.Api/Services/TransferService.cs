using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuickPay.Api.DTO;
using QuickPay.Api.Models;
using QuickPay.Api.Repositories;
using System.Linq.Expressions;
using System.Transactions;

namespace QuickPay.Api.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _repository;
        private readonly IWalletrepository _walletrepository;
        private readonly IPaymentIdempotencyRepository _paymentidempotencyrepository;
        public TransferService(ITransferRepository repository, IWalletrepository walletrepository, IPaymentIdempotencyRepository paymentIdempotancyRepository) 
        {
          _repository = repository;
          _walletrepository = walletrepository;
            _paymentidempotencyrepository = paymentIdempotancyRepository;

        }


        public async Task<TransferResultDTO> TransferMoneyAsync(int fromUserId, int toUserId, decimal amount, string idempotencykey)
        {    
            // STEP 1: Basic validations

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }

            if (fromUserId == toUserId)
            {
                throw new ArgumentException("Sender and receiver cannot be same");
            }

            if (string.IsNullOrWhiteSpace(idempotencykey))
            {
                throw new ArgumentException("Idempotency key is required");
            }

            // STEP 2: Check whether this key was already used

            var existing = await _paymentidempotencyrepository.GetByKeyAsync(idempotencykey);

            if (existing != null)
            {
                return await GetPreviousResult(existing, fromUserId, toUserId, amount);
            }

            // STEP 3: Fetch sender and receiver wallets

            var sender = await _repository.GetWalletByUserIdAsync(fromUserId);
            var receiver = await _repository.GetWalletByUserIdAsync(toUserId);

            if (sender == null)
            {
                throw new KeyNotFoundException("Sender wallet not found");
            }

            if (receiver == null)
            {
                throw new KeyNotFoundException("Receiver wallet not found");
            }

            try
            {
                // STEP 4: Start database transaction

                await using var transaction = await _repository.BeginTransactionsAsync();

                // STEP 5: Create and insert Pending idempotency record

                var payment = new PaymentIdempotency
                {
                    IdempotencyKey = idempotencykey,
                    FromUserId = fromUserId,
                    ToUserId = toUserId,
                    Amount = amount,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                };

                _paymentidempotencyrepository.Add(payment);

                // Save key first. Unique index protects against duplicate claims.
                await _repository.SaveChangesAsync();

                // STEP 6: Deduct sender balance

                bool deducted = await _repository.TryDeductBalanceAsync(sender.Id, amount);

                if (!deducted)
                {
                    throw new InvalidOperationException("Insufficient Balance");
                }

                // STEP 7: Credit receiver balance

                bool credited = await _walletrepository.AddWalletBalanceAsync(receiver.Id, amount);

                if (!credited)
                {
                    throw new InvalidOperationException("Payment failed");
                }

                // STEP 8: Create transfer record

                Transfer transfer = new Transfer
                {
                    FromWalletId = sender.Id,
                    ToWalletId = receiver.Id,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow
                };

                _repository.AddTransferAsync(transfer);

                // First save transfer to generate its database ID
                await _repository.SaveChangesAsync();

                // STEP 9: Mark idempotency record as Completed

                payment.TransferId = transfer.Id;
                payment.Status = "Completed";

                await _repository.SaveChangesAsync();

                // STEP 10: Commit all database changes

                await transaction.CommitAsync();

                // STEP 11: Return payment result

                return new TransferResultDTO
                {
                    TransferId = transfer.Id,
                    FromUserId = fromUserId,
                    ToUserId = toUserId,
                    Amount = amount,
                    CreateAt = transfer.CreatedAt,
                    Status = "Completed"
                };
            }
            catch (DbUpdateException ex) when (IsDuplicateKey(ex))
            {
                // Another request may have claimed the same key.
                // Fetch its committed result after transaction rollback.

                var duplicate = await _paymentidempotencyrepository.GetByKeyAsync(idempotencykey);

                if (duplicate == null)
                {
                    throw;
                }

                return await GetPreviousResult(duplicate, fromUserId, toUserId, amount);
            }
        }

        private async Task<TransferResultDTO> GetPreviousResult(PaymentIdempotency existing,int fromUserId, int toUserId, decimal amount)
        {
            if(existing.FromUserId != fromUserId || existing.ToUserId != toUserId || existing.Amount != amount)
            {
                throw new InvalidOperationException("Idempotency key already used with payment details");

            }

            if (existing.Status != "Completed" || existing.TransferId == null)
            {
                throw new InvalidOperationException("Payment with this key is not completed");

            
            }
            var result = await _repository.GetTransferResultByIdAsync(existing.TransferId.Value);

            if (result == null)
            {
                throw new InvalidOperationException("Previous transfer not found");

            }
            return result;
        
        }



      private bool IsDuplicateKey(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlexception && (sqlexception.Number == 2601 || sqlexception.Number == 2627);

        }
        public async Task<List<TransferHistoryDTO>> GetTransferHistoryAsync(int userId)
        {
            var transfer = await _repository.GetTransferHistoryAsync(userId);
            //Db se list me data lakr ke usko dto mein kaise daalein 
            //List ke har record ko uthao aur use naye format mein convert karo.
            
            
            return transfer;

        }
    }
}
