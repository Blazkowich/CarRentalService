﻿using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.RentalUnitOfWork;
using CarRental.Shared.CustomExceptions;
using System.ComponentModel.DataAnnotations;

namespace CarRental.BLL.Services;

public class VehicleService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : IVehicleService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        var allVehicles = await _rentalUnitOfWork.VehiclesRepository.GetAllAsync();
        return _mapper.Map<List<Vehicle>>(allVehicles);
    }

    public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
    {
        var getVehicleById = await _rentalUnitOfWork.VehiclesRepository
            .GetByIdAsync(vehicleId, default);
        return _mapper.Map<Vehicle>(getVehicleById);
    }

    public async Task<Vehicle> GetVehicleByNameAsync(string name)
    {
        var getVehicleByName = await _rentalUnitOfWork.VehiclesRepository
            .GetVehicleByNameAsync(name, default);
        return _mapper.Map<Vehicle>(getVehicleByName);
    }

    public async Task<List<Vehicle>> GetVehiclesByTypeAsync(VehicleTypeBLL type)
    {
        var getVehiclesByType = await _rentalUnitOfWork.VehiclesRepository
            .GetVehiclesByTypeAsync(_mapper.Map<VehicleTypeDAL>(type), default);
        return _mapper.Map<List<Vehicle>>(getVehiclesByType);
    }

    public async Task<List<Vehicle>> GetVehiclesByReservationTypeAsync(ReservationTypeBLL reservationType)
    {
        var getByReservationType = await _rentalUnitOfWork.VehiclesRepository
            .GetVehiclesByReservationTypeAsync(_mapper.Map<ReservationTypeDAL>(reservationType), default);
        return _mapper.Map<List<Vehicle>>(getByReservationType);
    }

    public async Task<Guid> AddVehicleAsync(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        var addVehicle = await _rentalUnitOfWork.VehiclesRepository.AddAsync(_mapper.Map<VehicleEntity>(vehicle));

        await _rentalUnitOfWork.SaveAsync(default);

        return addVehicle.Id;
    }

    public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
    {
        var updateVehicle = await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(_mapper.Map<VehicleEntity>(vehicle), default);
        await _rentalUnitOfWork.SaveAsync(default);
        return _mapper.Map<Vehicle>(updateVehicle);
    }

    public async Task DeleteVehicleAsync(Guid vehicleId)
    {
        await _rentalUnitOfWork.VehiclesRepository.DeleteAsync(vehicleId, default);
        await _rentalUnitOfWork.SaveAsync(default);
    }

    public async Task<List<Vehicle>> GetAllAvailableVehiclesAsync()
    {
        var getAllAvailables = await _rentalUnitOfWork.VehiclesRepository.GetAllAvailableVehicles();
        return _mapper.Map<List<Vehicle>>(getAllAvailables);
    }

    public List<string> GetEnumToString(Type enumType)
    {
        if (!enumType.IsEnum)
        {
            throw new BadRequestException($"{enumType.Name} is not an enum type.");
        }

        var vehicleTypes = Enum.GetValues(enumType)
                               .Cast<Enum>()
                               .Select(v => GetDisplayName(v))
                               .ToList();
        return vehicleTypes;
    }

    private static string GetDisplayName(Enum value)
    {
        var displayAttribute = value.GetType()
                                   .GetField(value.ToString())
                                   .GetCustomAttributes(typeof(DisplayAttribute), false)
                                   .SingleOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? value.ToString();
    }
}