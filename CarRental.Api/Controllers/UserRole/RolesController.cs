using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;
using CarRental.Service.Mapper.Services.Interfaces;
using CarRental.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.UserRole
{
    [ApiController]
    [Route("roles")]
    [Authorize(Roles = "Admin")]
    public class RolesController(IRolesMapped rolesMapped) : ControllerBase
    {
        private readonly IRolesMapped _rolesMapped = rolesMapped;

        [HttpGet]
        public async Task<List<RolesResponse>> GetAllRoles()
        {
            return await _rolesMapped.GetAllRolesAsync();
        }

        [HttpGet("{id}")]
        public async Task<RolesResponse> GetRoleById(Guid id)
        {
            return await _rolesMapped.GetRoleByIdAsync(id);
        }

        [HttpGet("permissions")]
        public async Task<List<string>> GetPermissions()
        {
            return await _rolesMapped.GetAllPermissionsAsync();
        }

        [HttpGet("{id}/permissions")]
        public async Task<List<string>> GetPermissionByRoleId(Guid id)
        {
            return await _rolesMapped.GetPermissionsByRoleIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(CreateRoleRequest newRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is Invalid");
            }

            var addedRole = await _rolesMapped.AddRoleAsync(newRole);
            return Ok(addedRole);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            await _rolesMapped.DeleteRoleAsync(roleId);
            return Ok();
        }

        [HttpPut]
        public async Task<RolesResponse> UpdateRole(UpdateRoleRequest newRole)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Model Is Invalid");
            }

            return await _rolesMapped.UpdateRoleAsync(newRole);
        }
    }
}
