using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Support;

[ApiController]
[Route("email")]
[Authorize]
public class EmailController(IEmailMapped emailMapped) : ControllerBase
{
    private readonly IEmailMapped _emailMapped = emailMapped;

    [HttpPost("send")]
    public async Task<IActionResult> SendEmailToSupport([FromBody] EmailRequest email)
    {
        await _emailMapped.SendEmailAsync(email.Subject, email.Message, HttpContext.User);
        return Ok();
    }
}
