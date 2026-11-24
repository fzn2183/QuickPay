using QuickPay.Api.Models;
using QuickPay.Api.Repositories;
namespace QuickPay.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUserAsync(User user)
        {
          await _repository.CreateUserAsync(user);
            
        }


        public async Task<List<User>> GetAllUserAsync()
        {
            return await _repository.GetAllUsersAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);

        }

        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateUserAsync(user);


        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteUserAsync(id);

        }
    }

}

