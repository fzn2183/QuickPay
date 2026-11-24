using QuickPay.Api.DTO;
using QuickPay.Api.Models;
namespace QuickPay.Api.Repositories
{
    public interface IPaymentIdempotencyRepository
    {
        Task<PaymentIdempotency?>GetByKeyAsync(string key);

        void Add(PaymentIdempotency payment);
      
    }
}
