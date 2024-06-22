using CarRental.BLL.Models.Settings;
using CarRental.BLL.Services;
using CarRental.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.BLL.DependencyInjections;

public static class CarRentalBLLServiceExtension
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services, AuthApiSettings authSettings)
    {
        if (authSettings is null)
        {
            throw new ArgumentNullException(nameof(authSettings), "AuthApiSettings cannot be null.");
        }

        services.AddSingleton(authSettings);


        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IReservationScheduler, ReservationScheduler>();

        return services;
    }
}
