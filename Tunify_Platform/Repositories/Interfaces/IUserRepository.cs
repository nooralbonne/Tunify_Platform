using Tunify_Platform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user); // Changed return type to Task<User>
        Task<User> UpdateUserAsync(int id, User user); // Added id parameter
        Task DeleteUserAsync(int id);
    }
}
