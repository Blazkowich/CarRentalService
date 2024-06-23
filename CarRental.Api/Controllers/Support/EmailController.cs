using CarRental.Support.Email.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Support;

[ApiController]
[Route("email")]
[Authorize]
public class EmailController(IEmailService emailService) : ControllerBase
{
    private readonly IEmailService _emailService = emailService;

    [HttpPost("send")]
    public async Task<IActionResult> SendEmailToSupport(string subject, string message)
    {
        await _emailService.SendEmailAsync(subject, message, HttpContext.User);

        return Ok();
    }
}
