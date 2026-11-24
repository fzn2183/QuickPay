using Microsoft.EntityFrameworkCore;
using QuickPay.Api.Data;
using QuickPay.Api.DTO;
using QuickPay.Api.Models;

namespace QuickPay.Api.Repositories
{
    public class PaymentIdempotencyRepository:IPaymentIdempotencyRepository
    {
        private readonly AppDbContext _context;
        public PaymentIdempotencyRepository(AppDbContext context)
        {
            _context = context;

        }


      
        public async Task<PaymentIdempotency?> GetByKeyAsync(string key)
        {
            return await _context.PaymentIdempotencies.AsNoTracking().FirstOrDefaultAsync(p => p.IdempotencyKey == key);
        }
        public void Add(PaymentIdempotency payment)
        {
            _context.PaymentIdempotencies.Add(payment);
        }

    }
}
