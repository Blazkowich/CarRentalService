﻿using Api.Bootstrapping.CustomExceptions;
using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

internal class BookingService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : IBookingService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        var getAllBooking = await _rentalUnitOfWork.BookingsRepository.GetAllAsync();
        return await MapAndCalculateTotalPricesAsync(_mapper.Map<List<Booking>>(getAllBooking));
    }

    public async Task<List<Booking>> GetBookingsByConditionAsync(BookingTypeBLL bookingCondition)
    {
        var bookingByCondition = await _rentalUnitOfWork.BookingsRepository
            .GetBookingsByConditionAsync(_mapper.Map<BookingTypeDAL>(bookingCondition));

        return _mapper.Map<List<Booking>>(bookingByCondition);
    }

    public async Task<Booking> ReserveVehicleAsync(Vehicle vehicle, DateTime startDate, int durationInDays)
    {
        if (await CheckIfVehicleReserved(vehicle.Id))
        {
            throw new BadRequestException($"Car {vehicle.Name} Already Reserved");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            VehicleId = vehicle.Id,
            CustomerId = Guid.NewGuid(), // This should come from the context, e.g., the logged-in user
            BookingDate = DateTime.UtcNow,
            StartDate = startDate,
            EndDate = startDate.AddDays(durationInDays),
            BookingCondition = BookingTypeBLL.Reserved
        };

        vehicle.ReservationType = ReservationTypeBLL.Free;

        await _rentalUnitOfWork.BookingsRepository.AddAsync(_mapper.Map<BookingEntity>(booking));
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(_mapper.Map<VehicleEntity>(vehicle));
        await _rentalUnitOfWork.SaveAsync();

        // Comment: Implement a method to send a message to the user when the reservation start date arrives.

        return booking;
    }

    public async Task<Booking> BookVehicleAsync(Vehicle vehicle, int durationInDays)
    {
        if (await CheckIfVehicleReserved(vehicle.Id))
        {
            throw new BadRequestException($"Car {vehicle.Name} Already Reserved");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            VehicleId = vehicle.Id,
            CustomerId = Guid.NewGuid(), // This should come from the context, e.g., the logged-in user
            BookingDate = DateTime.UtcNow,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(durationInDays),
            BookingCondition = BookingTypeBLL.Active
        };

        vehicle.ReservationType = ReservationTypeBLL.Reserved;

        await _rentalUnitOfWork.BookingsRepository.AddAsync(_mapper.Map<BookingEntity>(booking));
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(_mapper.Map<VehicleEntity>(vehicle));
        await _rentalUnitOfWork.SaveAsync();

        return booking;
    }

    public async Task<Booking> CancelBookingAsync(Booking booking)
    {
        var bookingEntity = await _rentalUnitOfWork.BookingsRepository.GetByIdAsync(booking.Id);
        if (bookingEntity == null)
        {
            throw new NotFoundException($"Booking with ID {booking.Id} not found");
        }

        bookingEntity.BookingCondition = BookingTypeDAL.Cancelled;

        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(booking.VehicleId);
        vehicle.ReservationType = ReservationTypeDAL.Free;

        await _rentalUnitOfWork.BookingsRepository.UpdateAsync(bookingEntity);
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
        await _rentalUnitOfWork.SaveAsync();

        return _mapper.Map<Booking>(bookingEntity);
    }

    #region Private Methods
    private async Task<bool> CheckIfVehicleReserved(Guid vehicleId)
    {
        var getVehicleById = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(vehicleId);

        return getVehicleById.ReservationType == ReservationTypeDAL.Reserved;
    }

    private async Task<List<Booking>> MapAndCalculateTotalPricesAsync(List<Booking> bookings)
    {

        foreach (var booking in bookings)
        {
            var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(booking.VehicleId);

            booking.TotalPrice = CalculateTotalPrice(vehicle.Price, booking.StartDate, booking.EndDate);

            if (DateTime.UtcNow > booking.EndDate)
            {
                booking.BookingCondition = BookingTypeBLL.Finished;
                await _rentalUnitOfWork.SaveAsync();

                await ReturnCarToSalonAsync(_mapper.Map<Vehicle>(vehicle));
            }
        }

        return bookings;
    }

    private static double CalculateTotalPrice(double vehiclePricePerDay, DateTime startDate, DateTime endDate)
    {
        var totalDays = (endDate - startDate).TotalDays;
        return Math.Round(totalDays * vehiclePricePerDay, 2);
    }

    private async Task ReturnCarToSalonAsync(Vehicle vehicle)
    {
        vehicle.ReservationType = ReservationTypeBLL.Free;
        await _rentalUnitOfWork.VehiclesRepository
            .UpdateAsync(_mapper.Map<VehicleEntity>(vehicle));

        await _rentalUnitOfWork.SaveAsync();
    }
    #endregion
}
