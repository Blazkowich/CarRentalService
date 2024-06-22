using CarRental.Support.Email.Services;
using CarRental.Support.Email.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Support.Email.DependencyInjection;

public static class CarRentalEmailServiceExtension
{
    public static IServiceCollection AddEmailBLLServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }
}
