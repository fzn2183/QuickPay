using QuickPay.Api.DTO;

namespace QuickPay.Api.Services
{
    public interface ITransferService
    {
        Task <TransferResultDTO>TransferMoneyAsync(int fromWalletId, int toWalletId, decimal amount,string idempotencykey);
        Task<List<TransferHistoryDTO>> GetTransferHistoryAsync(int userId);

       
    }
}
