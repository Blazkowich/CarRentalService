using Api.Bootstrapping.CustomExceptions;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Support.Email.Services.Interface;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace CarRental.Support.Email.Services;
public class EmailService(IUserService userService) : IEmailService
{
    private readonly IUserService _userService = userService;

    public async Task SendEmailAsync(string subject, string body, ClaimsPrincipal user)
    {
        var customerName = user.FindFirst(ClaimTypes.Name)?.Value ??
            throw new BadRequestException("Customer Id not found in claims.");

        var customer = await _userService.GetUserByUserNameAsync(customerName);

        try
        {
            var senderEmail = customer.Email;
            var senderPassword = "yguy kttr wvhz wqqj";

            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add("oiluridze@gmail.com");

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
