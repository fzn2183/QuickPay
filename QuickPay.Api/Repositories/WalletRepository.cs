using Microsoft.EntityFrameworkCore;
using QuickPay.Api.Data;
using QuickPay.Api.Models;

namespace QuickPay.Api.Repositories
{
    public class WalletRepository:IWalletrepository
    {
        private readonly AppDbContext _context;
        public WalletRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<Wallet?> GetWalletByUserIdAsync(int userId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(i => i.UserId == userId);

            return wallet;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);
            if(usr == null)
            {
                return true;
            }
            return false;
        }


        public async Task CreateWalletAsync(Wallet wallet)
        {
            if(wallet == null)
            {
                throw new ArgumentNullException(nameof(wallet));

            }
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

        }


        public async Task<bool> AddWalletBalanceAsync(int walletId, decimal amount)
        {
            if (amount > 0)
            {
                var affectedrows = await _context.Wallets.Where(i => i.Id == walletId).
                    ExecuteUpdateAsync(setters => setters.SetProperty(w => w.Balance,
                    w => w.Balance + amount));
                return affectedrows == 1;
            }

            return false;
        }

      

    }
}
