using CarRental.Auth.BLL.AutoMapper;
using CarRental.Auth.BLL.DependencyInjections;
using CarRental.Auth.DAL.DependencyInjections;
using CarRental.BLL.AutoMapper;
using CarRental.BLL.DependencyInjections;
using CarRental.DAL.DependencyInjections;
using CarRental.Service.Mapper.Mapping;
using CarRental.Service.Mapper.Mapping.Auth;
using CarRental.Service.Mapper.Services;
using CarRental.Service.Mapper.Services.Interfaces;
using CarRental.Support.Chat.DependencyInjection;
using CarRental.Support.Email.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarRental.Service.Mapper.DependencyInjections;

public static class CarRentalDependencies
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthMapped, AuthMapped>();
        services.AddScoped<IBookingMapped, BookingMapped>();
        services.AddScoped<IChatMapped, ChatMapped>();
        services.AddScoped<IEmailMapped, EmailMapped>();
        services.AddScoped<IRolesMapped, RolesMapped>();
        services.AddScoped<IUserMapped, UserMapped>();
        services.AddScoped<IVehicleMapped, VehicleMapped>();

        services.AddAutoMapper(typeof(AutomapperProfile));
        services.AddAutoMapper(typeof(AutomapperProfileBLL));
        services.AddAutoMapper(typeof(AuthMapperBLL));
        services.AddAutoMapper(typeof(AuthApiAutoMapper));

        services.AddDALRepositories(configuration);
        services.AddAuthDALRepositories(configuration);

        services.AddBLLServices();
        services.AddAuthBLLServices();

        services.AddEmailBLLServices();

        services.AddSupportChatBLLServices();

        return services;
    }
}
