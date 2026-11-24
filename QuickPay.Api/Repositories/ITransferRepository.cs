using Microsoft.EntityFrameworkCore.Storage;
using QuickPay.Api.Data;
using QuickPay.Api.Models;
using QuickPay.Api.DTO;

namespace QuickPay.Api.Repositories
{
    public interface ITransferRepository
    {
        Task<Wallet?> GetWalletByUserIdAsync(int UserId);
        void AddTransferAsync(Transfer transfer);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionsAsync();
        Task<bool> TryDeductBalanceAsync(int walletId, decimal amount);
        Task<List<TransferHistoryDTO>> GetTransferHistoryAsync(int userId);

        Task<TransferResultDTO?> GetTransferResultByIdAsync(int transferId);
    }
}
