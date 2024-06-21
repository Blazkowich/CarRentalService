using CarRental.Auth.BLL.Models;
using System.Security.Claims;

namespace CarRental.Auth.BLL.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();

    Task<User> GetUserByIdAsync(Guid id);

    Task<Guid> AddUserAsync(User user);

    Task<User> UpdateUserAsync(User user);

    Task DeleteUserAsync(Guid userId);

    Task<List<Roles>> GetRolesByUserIdAsync(Guid userId);

    Task<List<Permissions>> GetPermissionsByRoleIdAsync(Guid userId);

    Task<string> Login(User user);

    Task<(User UserEntity, string Token)> Register(User user);

    Task<bool> CheckPageAccess(string targetPage, string targetId, ClaimsPrincipal user);
}

