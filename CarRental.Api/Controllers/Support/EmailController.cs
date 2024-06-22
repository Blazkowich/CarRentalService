using CarRental.Support.Email.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Support;

[ApiController]
[Route("email")]
public class EmailController(IEmailService emailService) : ControllerBase
{
    private readonly IEmailService _emailService = emailService;

    [HttpPost("send")]
    public async Task<IActionResult> SendEmailToSupport(string subject, string message)
    {
        await _emailService.SendEmailAsync(subject, message);

        return Ok();
    }
}
