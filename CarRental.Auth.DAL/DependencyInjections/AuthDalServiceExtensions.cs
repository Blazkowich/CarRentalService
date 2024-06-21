using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Repositories;
using CarRental.Auth.DAL.Repositories.AuthUnitOfWork;
using CarRental.Auth.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Auth.DAL.DependencyInjections;

public static class AuthDalServiceExtensions
{
    public static IServiceCollection AddAuthDALRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarRentalAuthDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CarRentalAuthConnectionString"));
        });

        services.AddScoped<DbContext, CarRentalAuthDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IPermissionsRepository, PermissionsRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();

        return services;
    }

}
