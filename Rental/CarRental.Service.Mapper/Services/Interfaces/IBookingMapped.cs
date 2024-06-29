using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services.Interfaces
{
    public interface IBookingMapped
    {
        Task<List<BookingResponseFull>> GetAllBookingsAsync();
        Task<List<BookingResponseFull>> GetBookingsByConditionAsync(BookingTypeApi bookingCondition);
        Task<BookingResponseFull> ReserveVehicleAsync(Guid vehicleId, DateTime startDate, int duration, ClaimsPrincipal user);
        Task<BookingResponseFull> BookVehicleAsync(BookingRequest bookingRequest, ClaimsPrincipal user);
        Task<BookingResponseFull> CancelBookingOrReservationAsync(BookingRequest bookingRequest);
        Task<string> VehicleAvailableFromAsync(Guid vehicleId);
        Task<List<BookingResponseFull>> GetBookingHistoryByUserAsync(Guid userId);
        Task<List<BookingResponseFull>> GetActiveBookingsByUserAsync(Guid userId);
        List<string> GetReservationTypes();
    }
}
