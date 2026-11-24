using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuickPay.Api.Data;
using QuickPay.Api.DTO;
using QuickPay.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickPay.Api.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly AppDbContext _context;
        
        public TransferRepository(AppDbContext context)
        {
            _context = context;
        }
        //Method 1 Get Wallets 
        public async Task<Wallet?> GetWalletByUserIdAsync(int UserId)

        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(i => i.UserId == UserId);
            return wallet;

        }

        //Method 2 Add Transfer Async 
        public void AddTransferAsync(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
        }


        //Method 3 Save changesAsync
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        //Method 4 - Database Transaction
        public async Task<IDbContextTransaction> BeginTransactionsAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        //Method 5 -Validation for Balance Deduction

        public async Task<bool> TryDeductBalanceAsync(int walletId, decimal amount)
        {
            var Affectedrows = await  _context.Wallets.Where(i => i.Id == walletId && i.Balance >= amount)
                 .ExecuteUpdateAsync(setters => setters.SetProperty(w => w.Balance,
                     w => w.Balance - amount));
            
            return Affectedrows == 1;


        }
        //Method -6 Add Money in wallet


        public async Task<List<TransferHistoryDTO>> GetTransferHistoryAsync(int userId)
        {
            //Fetch Wallet of User
            var wallet = await GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                throw new KeyNotFoundException("Wallet Not Found");
            }
            var transfer = await (from t in _context.Transfers
            //Sender ka wallet
            join sender in _context.Wallets on t.FromWalletId equals sender.Id

            //Receiver ka wallet 
           join reciever in _context.Wallets on t.ToWalletId equals reciever.Id

            //Sirf current user ki transaction
           where t.FromWalletId == wallet.Id || t.ToWalletId == wallet.Id
            orderby t.CreatedAt descending

            select new TransferHistoryDTO
            {
                id = t.Id,
                FromUserId = sender.UserId,
                ToUserId = reciever.UserId,
                 Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                Type = sender.UserId == userId ? "Sent" : "Recieved"
            }).ToListAsync();
            return transfer;

        }

        public async Task<TransferResultDTO?> GetTransferResultByIdAsync(int transferId)
        {
            var result = await (
                from t in _context.Transfers
                join sender in _context.Wallets
                    on t.FromWalletId equals sender.Id
                join receiver in _context.Wallets
                    on t.ToWalletId equals receiver.Id
                where t.Id == transferId
                select new TransferResultDTO
                {
                    TransferId = t.Id,
                    FromUserId = sender.UserId,
                    ToUserId = receiver.UserId,
                    Amount = t.Amount,
                    CreateAt = t.CreatedAt,
                    Status = "Completed"
                }
            ).FirstOrDefaultAsync();

            return result;
        }


    }
}
