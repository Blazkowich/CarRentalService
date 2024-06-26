using CarRental.Auth.BLL.Models;
using System.Security.Claims;

namespace CarRental.Auth.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<(string Token, string FirstName, string Id, string Role)> Login(User user);

    Task<(User UserEntity, string Token)> Register(User user);

    Task LogOut(ClaimsPrincipal user);
}
