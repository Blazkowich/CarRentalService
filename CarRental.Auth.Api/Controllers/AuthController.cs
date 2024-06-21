using AutoMapper;
using CarRental.Auth.Api.ApiModels.Request;
using CarRental.Auth.Api.ApiModels.Response;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Auth.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userService.Login(_mapper.Map<User>(user));

        return Ok(new { Token = result });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(userRequest);
        var (userEntity, token) = await _userService.Register(user);

        return Ok(new { User = _mapper.Map<UserResponse>(userEntity), Token = token });
    }
}

