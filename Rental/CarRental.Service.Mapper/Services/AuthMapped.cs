using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.Services.Interfaces;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services;

internal class AuthMapped : IAuthMapped
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthMapped(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<(string Token, string FirstName, string Id, string Role)> LoginUserAsync(LoginUserRequest user)
    {
        var userModel = _mapper.Map<User>(user);
        return await _authService.Login(userModel);
    }

    public async Task<(User UserEntity, string Token)> RegisterUserAsync(RegisterUserRequest userRequest)
    {
        var user = _mapper.Map<User>(userRequest);
        return await _authService.Register(user);
    }

    public async Task LogOutUserAsync(ClaimsPrincipal user)
    {
        await _authService.LogOut(user);
    }
}
