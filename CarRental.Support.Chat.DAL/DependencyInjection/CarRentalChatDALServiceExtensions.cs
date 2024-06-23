using CarRental.Support.Chat.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Support.Chat.DAL.DependencyInjection;

public static class CarRentalChatDALServiceExtensions
{
    public static IServiceCollection AddSupportChatDALRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();

        services.AddDbContext<CarRentalChatDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CarRentalChatConnectionString")));

        return services;
    }
}
