using OOTF.Shopping.Models;

namespace OOTF.Shopping.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> AddUserAsync(User newUser);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<User>GetUserByUsernameAndPassword(string username, string password);
        Task<User>GetUserByUsername(string? name);
        Task<User>CreateUser(User user);
    }
}
