using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;
using CarRental.Service.Mapper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Renting
{
    [ApiController]
    [Route("booking")]
    [Authorize]
    public class BookingController(IBookingMapped bookingMapped) : ControllerBase
    {
        private readonly IBookingMapped _bookingMapped = bookingMapped;

        [HttpGet]
        public async Task<List<BookingResponseFull>> GetAllBookings()
        {
            return await _bookingMapped.GetAllBookingsAsync();
        }

        [HttpGet("condition")]
        public async Task<List<BookingResponseFull>> GetBookingsByCondition([FromQuery] BookingTypeApi bookingCondition)
        {
            return await _bookingMapped.GetBookingsByConditionAsync(bookingCondition);
        }

        [HttpPost("reservation")]
        public async Task<BookingResponseFull> ReserveVehicle(Guid vehicleId, [FromQuery] DateTime startDate, int duration)
        {
            return await _bookingMapped.ReserveVehicleAsync(vehicleId, startDate, duration, HttpContext.User);
        }

        [HttpPost("book")]
        public async Task<BookingResponseFull> BookVehicle([FromBody] BookingRequest bookingRequest)
        {
            return await _bookingMapped.BookVehicleAsync(bookingRequest, HttpContext.User);
        }

        [HttpPost("cancel")]
        public async Task<BookingResponseFull> CancelBookingOrReservation([FromBody] BookingRequest bookingRequest)
        {
            return await _bookingMapped.CancelBookingOrReservationAsync(bookingRequest);
        }

        [HttpGet("availableFrom/{vehicleId}")]
        [AllowAnonymous]
        public async Task<string> VehicleAvailableFrom(Guid vehicleId)
        {
            return await _bookingMapped.VehicleAvailableFromAsync(vehicleId);
        }

        [HttpGet("bookingHistoryByUser/{userId}")]
        public async Task<List<BookingResponseFull>> GetBookingHistoryByUser(Guid userId)
        {
            return await _bookingMapped.GetBookingHistoryByUserAsync(userId);
        }

        [HttpGet("activeBookingByUser/{userId}")]
        public async Task<List<BookingResponseFull>> GetActiveBookingsByUser(Guid userId)
        {
            return await _bookingMapped.GetActiveBookingsByUserAsync(userId);
        }

        [HttpGet("getReservationTypes")]
        [AllowAnonymous]
        public List<string> GetReservationTypes()
        {
            return _bookingMapped.GetReservationTypes();
        }
    }
}
