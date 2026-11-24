using QuickPay.Api.Models;
namespace QuickPay.Api.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
