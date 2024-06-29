using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;
using CarRental.Service.Mapper.Services.Interfaces;
using CarRental.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.UserRole
{
    [ApiController]
    [Route("users")]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserMapped userMapped) : ControllerBase
    {
        private readonly IUserMapped _userMapped = userMapped;

        [HttpGet]
        public async Task<List<UserResponse>> GetAllUsers()
        {
            return await _userMapped.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserResponse> GetUserById(Guid id)
        {
            return await _userMapped.GetUserByIdAsync(id);
        }

        [HttpGet("byName/{name}")]
        public async Task<Guid> GetUserIdByName(string name)
        {
            return await _userMapped.GetUserIdByNameAsync(name);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("The provided model is not valid.");
            }

            var result = await _userMapped.AddUserAsync(user);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _userMapped.DeleteUserAsync(userId);
            return Ok();
        }

        [HttpPut]
        public async Task<UserResponse> UpdateUser(UpdateUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("The provided model is not valid.");
            }

            return await _userMapped.UpdateUserAsync(user);
        }

        [HttpGet("{id}/roles")]
        public async Task<List<RolesResponse>> GetRolesByUserId(Guid id)
        {
            return await _userMapped.GetRolesByUserIdAsync(id);
        }
    }
}
