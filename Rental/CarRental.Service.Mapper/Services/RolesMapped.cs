using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;
using CarRental.Service.Mapper.Services.Interfaces;

namespace CarRental.Service.Mapper.Services
{
    internal class RolesMapped : IRolesMapped
    {
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;

        public RolesMapped(IRolesService rolesService, IMapper mapper)
        {
            _rolesService = rolesService;
            _mapper = mapper;
        }

        public async Task<List<RolesResponse>> GetAllRolesAsync()
        {
            var roles = await _rolesService.GetAllRolesAsync();
            return _mapper.Map<List<RolesResponse>>(roles);
        }

        public async Task<RolesResponse> GetRoleByIdAsync(Guid id)
        {
            var role = await _rolesService.GetRoleByIdAsync(id);
            return _mapper.Map<RolesResponse>(role);
        }

        public async Task<List<string>> GetAllPermissionsAsync()
        {
            return await _rolesService.GetAllPermissionsAsync();
        }

        public async Task<List<string>> GetPermissionsByRoleIdAsync(Guid id)
        {
            return await _rolesService.GetPermissionsByRoleIdAsync(id);
        }

        public async Task<RolesResponse> AddRoleAsync(CreateRoleRequest newRole)
        {
            var role = _mapper.Map<Roles>(newRole);
            var addedRole = await _rolesService.AddRoleAsync(role);
            return _mapper.Map<RolesResponse>(addedRole);
        }

        public Task DeleteRoleAsync(Guid roleId)
        {
            return _rolesService.DeleteRoleAsync(roleId);
        }

        public async Task<RolesResponse> UpdateRoleAsync(UpdateRoleRequest newRole)
        {
            var role = _mapper.Map<Roles>(newRole);
            var updatedRole = await _rolesService.UpdateRoleAsync(role);
            return _mapper.Map<RolesResponse>(updatedRole);
        }
    }
}
