using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using System.Security.Claims;

namespace CarRental.BLL.Services.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetAllBookingsAsync();

    Task<List<Booking>> GetBookingsByConditionAsync(BookingTypeBLL bookingCondition);

    Task<Booking> ReserveVehicleAsync(Guid vehicleId, DateTime startDate, int durationInDays, ClaimsPrincipal user);

    Task<Booking> BookVehicleAsync(Guid vehicleId, int durationInDays, ClaimsPrincipal user);

    Task<Booking> CancelBookingAsync(Booking booking);
}
