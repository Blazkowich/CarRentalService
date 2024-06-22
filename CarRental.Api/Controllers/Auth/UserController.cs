using Api.Bootstrapping.CustomExceptions;
using AutoMapper;
using CarRental.Api.ApiModels.Auth.Request;
using CarRental.Api.ApiModels.Auth.Response;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Auth;

[ApiController]
[Route("users")]
[Authorize(Roles = "Admin")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<UserResponse>> GetAllUsers()
    {
        var response = await _userService.GetAllUsersAsync();

        return _mapper.Map<List<UserResponse>>(response);
    }

    [HttpGet("{id}")]
    public async Task<UserResponse> GetUserById(Guid id)
    {
        var response = await _userService.GetUserByIdAsync(id);

        return _mapper.Map<UserResponse>(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var result = await _userService.AddUserAsync(_mapper.Map<User>(user));

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _userService.DeleteUserAsync(userId);

        return Ok();
    }

    [HttpPut]
    public async Task<UserResponse> UpdateUser(UpdateUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var result = await _userService.UpdateUserAsync(_mapper.Map<User>(user));

        return _mapper.Map<UserResponse>(result);
    }

    [HttpGet("{id}/roles")]
    public async Task<List<RolesResponse>> GetRolesByUserId(Guid id)
    {
        var result = await _userService.GetRolesByUserIdAsync(id);

        return _mapper.Map<List<RolesResponse>>(result);
    }
}

