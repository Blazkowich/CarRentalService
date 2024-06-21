using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

internal class BookingService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : IBookingService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        var getAllBooking = await _rentalUnitOfWork.BookingsRepository.GetAllAsync();
        return MapAndCalculateTotalPrices(getAllBooking);
    }

    public async Task<List<Booking>> GetOverduedBookingsAsync()
    {
        var getOverdued = await _rentalUnitOfWork.BookingsRepository.GetOverduedBookingsAsync();
        return MapAndCalculateTotalPrices(getOverdued);
    }

    #region Private Methods

    private List<Booking> MapAndCalculateTotalPrices(List<BookingEntity> bookings)
    {
        var bookingsDto = _mapper.Map<List<Booking>>(bookings);

        foreach (var booking in bookingsDto)
        {
            var getVehicle = _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(booking.VehicleId);
            booking.TotalPrice = CalculateTotalPrice(getVehicle.Result.Price, booking.StartDate, booking.EndDate);
        }

        return bookingsDto;
    }

    private static double CalculateTotalPrice(double vehiclePricePerDay, DateTime startDate, DateTime endDate)
    {
        if (DateTime.UtcNow <= endDate)
        {
            var totalDays = (endDate - startDate).TotalDays;
            return Math.Round(totalDays * vehiclePricePerDay, 2);
        }
        else
        {
            var standardDays = (endDate - startDate).TotalDays;
            var overduedDays = (DateTime.UtcNow - endDate).TotalDays;

            var totalPrice = (standardDays * vehiclePricePerDay) + (overduedDays * vehiclePricePerDay * 2);
            return Math.Round(totalPrice, 2);
        }
    }
    #endregion
}
