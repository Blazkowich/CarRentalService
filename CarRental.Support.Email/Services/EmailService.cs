using CarRental.Support.Email.Services.Interface;
using System.Net;
using System.Net.Mail;

namespace CarRental.Support.Email.Services;
public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string subject, string body)
    {
        try
        {
            var senderEmail = "a7x.otto@gmail.com";
            var senderPassword = "baki ekgw xvio faci";

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
