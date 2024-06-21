using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

internal class BookingService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : IBookingService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        var getAllBooking = await _rentalUnitOfWork.BookingsRepository.GetAllAsync();
        return _mapper.Map<List<Booking>>(getAllBooking);
    }

    #region Private Methods
    private static double CalculateTotalPrice(double dailyPrice, DateTime startDate, DateTime endDate)
    {
        int numberOfDays = (endDate - startDate).Days;
        return dailyPrice * numberOfDays;
    }
    #endregion
}
