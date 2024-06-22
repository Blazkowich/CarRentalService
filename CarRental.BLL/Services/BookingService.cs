using Api.Bootstrapping.CustomExceptions;
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

    public async Task<Booking> ReserveVehicleAsync(Guid vehicleId, DateTime startDate, int durationInDays)
    {
        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(vehicleId) ??
            throw new NotFoundException($"Car with ID {vehicleId} not found");

        if (await CheckIfVehicleReserved(vehicleId, startDate, startDate.AddDays(durationInDays)))
        {
            throw new BadRequestException($"Car {vehicle.Name} is already reserved during the specified period.");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            VehicleId = vehicleId,
            CustomerId = Guid.NewGuid(), // Need To Change
            BookingDate = DateTime.UtcNow,
            StartDate = startDate,
            EndDate = startDate.AddDays(durationInDays),
            BookingCondition = BookingTypeBLL.Reserved,
            TotalPrice = CalculateTotalPrice(vehicle.Price, startDate, startDate.AddDays(durationInDays)),
        };

        vehicle.ReservationType = ReservationTypeDAL.Free;

        await _rentalUnitOfWork.BookingsRepository.AddAsync(_mapper.Map<BookingEntity>(booking));
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
        await _rentalUnitOfWork.SaveAsync();

        // method to send a message to the user when the reservation start date arrives.

        return booking;
    }

    public async Task<Booking> BookVehicleAsync(Guid vehicleId, int durationInDays)
    {
        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(vehicleId) ??
            throw new NotFoundException($"Car with ID {vehicleId} not found");

        if (await CheckIfVehicleReserved(vehicle.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(durationInDays)))
        {
            throw new BadRequestException($"Car {vehicle.Name} Already Reserved");
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            VehicleId = vehicle.Id,
            CustomerId = Guid.NewGuid(), // Need to Change
            BookingDate = DateTime.UtcNow,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(durationInDays),
            BookingCondition = BookingTypeBLL.Active
        };

        vehicle.ReservationType = ReservationTypeDAL.Reserved;

        await _rentalUnitOfWork.BookingsRepository.AddAsync(_mapper.Map<BookingEntity>(booking));
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
        await _rentalUnitOfWork.SaveAsync();

        return booking;
    }

    public async Task<Booking> CancelBookingAsync(Booking booking)
    {
        var bookingEntity = await _rentalUnitOfWork.BookingsRepository.GetByIdAsync(booking.Id) ?? 
            throw new NotFoundException($"Booking with ID {booking.Id} not found");
        
        bookingEntity.BookingCondition = BookingTypeDAL.Cancelled;

        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(booking.VehicleId);
        vehicle.ReservationType = ReservationTypeDAL.Free;

        await _rentalUnitOfWork.BookingsRepository.UpdateAsync(bookingEntity);
        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
        await _rentalUnitOfWork.SaveAsync();

        return _mapper.Map<Booking>(bookingEntity);
    }

    #region Private Methods
    private async Task<bool> CheckIfVehicleReserved(Guid vehicleId, DateTime startDate, DateTime endDate)
    {
        var bookings = await _rentalUnitOfWork.BookingsRepository.GetBookingsByVehicleIdAsync(vehicleId);

        foreach (var booking in bookings)
        {
            if (booking.BookingCondition == BookingTypeDAL.Reserved || booking.BookingCondition == BookingTypeDAL.Active)
            {
                if (startDate < booking.EndDate && endDate > booking.StartDate)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private async Task<List<Booking>> MapAndCalculateTotalPricesAsync(List<Booking> bookings)
    {

        foreach (var booking in bookings)
        {
            var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(booking.VehicleId);

            booking.TotalPrice = CalculateTotalPrice(vehicle.Price, booking.StartDate, booking.EndDate);
        }

        return bookings;
    }

    private static double CalculateTotalPrice(double vehiclePricePerDay, DateTime startDate, DateTime endDate)
    {
        var totalDays = (endDate - startDate).TotalDays;
        return Math.Round(totalDays * vehiclePricePerDay, 2);
    }
    #endregion
}
