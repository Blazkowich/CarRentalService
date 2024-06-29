using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController(IAuthMapped authService) : ControllerBase
{
    private readonly IAuthMapped _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var (Token, FirstName, Id, Role) = await _authService.LoginUserAsync(user);

        return Ok(new { Token, FirstName, Id, Role });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (userEntity, token) = await _authService.RegisterUserAsync(userRequest);

        return Ok(new { User = userEntity, Token = token });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _authService.LogOutUserAsync(HttpContext.User);

        return Ok(new { message = "Successfully logged out." });
    }
}

