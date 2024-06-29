using CarRental.Shared.CustomExceptions;
using AutoMapper;
using CarRental.Api.ApiModels.Auth.Request;
using CarRental.Api.ApiModels.Auth.Response;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Auth;

[ApiController]
[Route("roles")]
[Authorize(Roles = "Admin")]
public class RolesController(IRolesService rolesService, IMapper mapper) : ControllerBase
{
    private readonly IRolesService _rolesService = rolesService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<RolesResponse>> GetAllRoles()
    {
        var response = await _rolesService.GetAllRolesAsync();

        return _mapper.Map<List<RolesResponse>>(response);
    }

    [HttpGet("{id}")]
    public async Task<RolesResponse> GetRoleById(Guid id)
    {
        var response = await _rolesService.GetRoleByIdAsync(id);

        return _mapper.Map<RolesResponse>(response);
    }

    [HttpGet("permissions")]
    public async Task<List<string>> GetPermissions()
    {
        return await _rolesService.GetAllPermissionsAsync();
    }

    [HttpGet("{id}/permissions")]
    public async Task<List<string>> GetPermissionByRoleId(Guid id)
    {
        return await _rolesService.GetPermissionsByRoleIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(CreateRoleRequest newRole)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is Invalid");
        }

        var requestForAdd = await _rolesService.AddRoleAsync(_mapper.Map<Roles>(newRole));

        return Ok(requestForAdd);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        await _rolesService.DeleteRoleAsync(roleId);

        return Ok();
    }

    [HttpPut]
    public async Task<RolesResponse> UpdateRole(UpdateRoleRequest newRole)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Model Is Invalid");
        }

        var updateRequest = await _rolesService.UpdateRoleAsync(_mapper.Map<Roles>(newRole));

        return _mapper.Map<RolesResponse>(updateRequest);
    }
}
