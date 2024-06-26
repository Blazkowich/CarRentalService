﻿using AutoMapper;
using CarRental.Api.ApiModels.Auth.Request;
using CarRental.Api.ApiModels.Auth.Response;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var (Token, FirstName, Id, Role) = await _authService.Login(_mapper.Map<User>(user));

        return Ok(new { Token, FirstName, Id , Role});
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest userRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(userRequest);
        var (userEntity, token) = await _authService.Register(user);

        return Ok(new { User = _mapper.Map<UserResponse>(userEntity), Token = token });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _authService.LogOut(HttpContext.User);

        return Ok(new { message = "Successfully logged out." });
    }
}

