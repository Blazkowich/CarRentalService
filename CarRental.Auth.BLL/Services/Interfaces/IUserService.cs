using CarRental.Auth.BLL.Models;

namespace CarRental.Auth.BLL.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();

    Task<User> GetUserByIdAsync(Guid id);

    Task<Guid> AddUserAsync(User user);

    Task<User> UpdateUserAsync(User user);

    Task DeleteUserAsync(Guid userId);

    Task<User> GetUserByUserNameAsync(string userName);

    Task<List<Roles>> GetRolesByUserIdAsync(Guid userId);

    Task<List<Permissions>> GetPermissionsByRoleIdAsync(Guid userId);
}

