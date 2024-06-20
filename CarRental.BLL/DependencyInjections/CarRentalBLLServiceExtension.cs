﻿using CarRental.BLL.Services;
using CarRental.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.BLL.DependencyInjections;

public static class CarRentalBLLServiceExtension
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleService, VehicleService>();
        return services;
    }
}
