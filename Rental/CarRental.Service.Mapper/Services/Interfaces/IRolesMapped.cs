using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;

namespace CarRental.Service.Mapper.Services.Interfaces
{
    public interface IRolesMapped
    {
        Task<List<RolesResponse>> GetAllRolesAsync();
        Task<RolesResponse> GetRoleByIdAsync(Guid id);
        Task<List<string>> GetAllPermissionsAsync();
        Task<List<string>> GetPermissionsByRoleIdAsync(Guid id);
        Task<RolesResponse> AddRoleAsync(CreateRoleRequest newRole);
        Task DeleteRoleAsync(Guid roleId);
        Task<RolesResponse> UpdateRoleAsync(UpdateRoleRequest newRole);
    }
}
