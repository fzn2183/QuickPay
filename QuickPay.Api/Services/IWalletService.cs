namespace QuickPay.Api.Services
{
    public interface IWalletService
    {
        Task CreateWalletAsync(int userId);
        Task DepositMoneyAsync(int userId, decimal amount);
        Task<decimal> GetWalletBalanceAsync(int userId);
    }
}
