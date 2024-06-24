using CarRental.Auth.BLL.Models;

namespace CarRental.Auth.BLL.Services.Interfaces;

public interface IRolesService
{
    Task<List<Roles>> GetAllRolesAsync();

    Task<Roles> GetRoleByIdAsync(Guid id);

    Task<Guid> AddRoleAsync(Roles role);

    Task<Roles> UpdateRoleAsync(Roles role);

    Task DeleteRoleAsync(Guid roleId);

    Task<List<string>> GetAllPermissionsAsync();

    Task<List<string>> GetPermissionsByRoleIdAsync(Guid roleId);
}

