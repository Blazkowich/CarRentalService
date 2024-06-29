using CarRental.Auth.BLL.Models;
using CarRental.Service.Mapper.DTO.Auth.Request;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services.Interfaces;

public interface IAuthMapped
{
    Task<(string Token, string FirstName, string Id, string Role)> LoginUserAsync(LoginUserRequest user);
    Task<(User UserEntity, string Token)> RegisterUserAsync(RegisterUserRequest userRequest);
    Task LogOutUserAsync(ClaimsPrincipal user);
}
