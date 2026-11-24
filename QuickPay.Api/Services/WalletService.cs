using QuickPay.Api.Models;
using QuickPay.Api.Repositories;

namespace QuickPay.Api.Services
{
    public class WalletService:IWalletService
    {
        private readonly IWalletrepository _repository;
        public WalletService(IWalletrepository repository)
        {
            _repository = repository;
        }
        public async Task CreateWalletAsync(int userId)
        {//Check User Async
            bool usr = await _repository.UserExistsAsync(userId);
            //give error when usr == null
            if(usr == true)
            { throw new KeyNotFoundException("User doesnot exists");        
            }
            //Check Wallet already exists
           var existswallet =  await _repository.GetWalletByUserIdAsync(userId);
            if(existswallet != null)
            {
                throw new InvalidOperationException("wallet already exists for user");
            }
            //New Wallet object with balance = 0
            Wallet wallet = new Wallet 
            {
                UserId = userId,
                Balance = 0
            };
            await _repository.CreateWalletAsync(wallet);


        }

        public async Task DepositMoneyAsync(int userId,decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount should be greate than zero");

            }

            var wallet = await _repository.GetWalletByUserIdAsync(userId);
            if (wallet == null) 
            {
                throw new InvalidOperationException("wallet not Found");

            }
           bool deposited =  await _repository.AddWalletBalanceAsync(wallet.Id  , amount);
            if (!deposited)
            {
                throw new InvalidOperationException("Deposit failed");
            }
        }
        
        public async Task<decimal> GetWalletBalanceAsync(int userId)
        {
            var wallet = await _repository.GetWalletByUserIdAsync(userId);
            if(wallet == null)
            {
                throw new KeyNotFoundException("Wallet Not Found");
            }

            return wallet.Balance;
        }


    }
}
