using CarRental.BLL.Models;

namespace CarRental.BLL.Services.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetAllBookingsAsync();

    Task<List<Booking>> GetOverduedBookingsAsync();
}
