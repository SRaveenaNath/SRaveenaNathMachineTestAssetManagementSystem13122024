using SRaveenaNathMachineTestAssetManagementSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRaveenaNathMachineTestAssetManagementSystem.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
    }
}
