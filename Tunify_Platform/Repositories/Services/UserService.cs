using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tunify_Platform.Repositories.Services
{
    public class UserService : IUserRepository
    {
        private readonly TunifyDbContext _context;

        public UserService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user; // Keeping the return of the added user
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var oldUser = await _context.Users.FindAsync(id);
            if (oldUser != null)
            {
                oldUser.Username = user.Username;
                oldUser.Email = user.Email;
                oldUser.Join_Date = user.Join_Date;
                oldUser.SubscriptionId = user.SubscriptionId;

                await _context.SaveChangesAsync();
                return oldUser;
            }
            return null;
        }
    }
}
