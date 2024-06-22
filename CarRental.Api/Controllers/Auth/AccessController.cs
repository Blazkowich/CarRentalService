﻿using CarRental.Api.ApiModels.Auth.Request;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Auth;


[ApiController]
[Route("access")]
public class AccessController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CheckPageAccess([FromBody] PageAccessRequest request)
    {
        bool hasAccess = await _userService.CheckPageAccess(request.TargetPage, request.TargetId, HttpContext.User);

        return !hasAccess ? Forbid() : Ok(new { AccessGranted = true });
    }
}


