using System.Security.Claims;

namespace CarRental.Service.Mapper.Services.Interfaces;

public interface IEmailMapped
{
    Task SendEmailAsync(string subject, string message, ClaimsPrincipal user);
}
