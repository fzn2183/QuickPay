using Microsoft.EntityFrameworkCore;
using QuickPay.Api.Data;
using QuickPay.Api.Models;
namespace QuickPay.Api.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task CreateUserAsync(User user)
        {
            if(user == null)
            {
                return;
            }
            _context.Users.Add(user);
           await _context.SaveChangesAsync();


        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            var list = await _context.Users.ToListAsync();
            
            return list;


        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

          
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteUserAsync(int id)
        {
            var user  = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            if (user == null) {
                return;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
