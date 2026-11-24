using QuickPay.Api.Models;

namespace QuickPay.Api.Repositories
{
    public interface IWalletrepository
    {
        Task<Wallet?> GetWalletByUserIdAsync(int UserId);
        Task<bool> UserExistsAsync(int UserId);
        Task CreateWalletAsync(Wallet wallet);
        Task<bool> AddWalletBalanceAsync(int walletId, decimal amount);
        
    }
}
