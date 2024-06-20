﻿using CarRental.DAL.Context;
using CarRental.DAL.Repositories.Interfaces;
using CarRental.DAL.Repositories.RentalUnitOfWork;
using CarRental.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CarRental.DAL.DependencyInjections;

public static class CarRentalDALServiceExtension
{
    public static IServiceCollection AddDALRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarRentalDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CarRentalConnectionString"));
        });

        // For CarRental.DAL.Common.BaseRepository DI
        services.AddScoped<DbContext, CarRentalDbContext>();

        // DI using Generic repository
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        services.AddScoped<IRentalUnitOfWork, RentalUnitOfWork>();

        return services;
    }

}
