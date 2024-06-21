using CarRental.Auth.BLL.Services;
using CarRental.Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Auth.BLL.DependencyInjections;

public static class AuthBLLServiceExtensions
{
    public static IServiceCollection AddAuthBLLServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

}
