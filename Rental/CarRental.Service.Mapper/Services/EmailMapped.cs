using CarRental.Service.Mapper.Services.Interfaces;
using CarRental.Support.Email.Services.Interface;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services
{
    internal class EmailMapped : IEmailMapped
    {
        private readonly IEmailService _emailService;

        public EmailMapped(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task SendEmailAsync(string subject, string message, ClaimsPrincipal user)
        {
            return _emailService.SendEmailAsync(subject, message, user);
        }
    }
}
