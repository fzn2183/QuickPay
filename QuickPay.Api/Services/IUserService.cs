using QuickPay.Api.Models;
namespace QuickPay.Api.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);

        Task<List<User>> GetAllUserAsync();
        Task<User> GetByIdAsync(int id); 
        Task UpdateUserAsync(User user);    
        Task DeleteUserAsync(int id );

    }

    
}
