using System.Security.Claims;

namespace CarRental.Support.Email.Services.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string subject, string message, ClaimsPrincipal user);
}