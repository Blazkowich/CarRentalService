using AutoMapper;
using CarRental.Api.ApiModels.Enum;
using CarRental.Api.ApiModels.Request;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Renting;

[ApiController]
[Route("booking")]
[Authorize]
public class BookingController(
    IBookingService bookingService,
    IVehicleService vehicleService,
    IMapper mapper) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<BookingResponseFull>> GetAllBookings()
    {
        var bookingResponse = await _bookingService.GetAllBookingsAsync();
        return _mapper.Map<List<BookingResponseFull>>(bookingResponse);
    }

    [HttpGet("condition")]
    public async Task<List<BookingResponseFull>> GetBookingsByCondition([FromQuery] BookingTypeApi bookingCondition)
    {
        var getBookingByCondition = await _bookingService
            .GetBookingsByConditionAsync(_mapper.Map<BookingTypeBLL>(bookingCondition));

        return _mapper.Map<List<BookingResponseFull>>(getBookingByCondition);
    }

    [HttpPost("reservation")]
    public async Task<BookingResponseFull> ReserveVehicle(Guid vehicleId, [FromQuery] DateTime startDate, int duration)
    {
        var reserveVehicle = await _bookingService.ReserveVehicleAsync(vehicleId, startDate, duration, HttpContext.User);

        return _mapper.Map<BookingResponseFull>(reserveVehicle);
    }

    [HttpPost("book")]
    public async Task<BookingResponseFull> BookVehicle([FromBody] BookingRequest bookingRequest)
    {
        var bookVehicle = await _bookingService.BookVehicleAsync(bookingRequest.VehicleId, bookingRequest.Duration, HttpContext.User);

        return _mapper.Map<BookingResponseFull>(bookVehicle);
    }

    [HttpPost("cancel")]
    public async Task<BookingResponseFull> CancelBookingOrReservation([FromBody] BookingRequest bookingRequest)
    {
        var cancel = await _bookingService.CancelBookingAsync(_mapper.Map<Booking>(bookingRequest));

        return _mapper.Map<BookingResponseFull>(cancel);
    }

    [HttpGet("availableFrom/{vehicleId}")]
    [AllowAnonymous]
    public async Task<string> VehicleAvailableFrom(Guid vehicleId)
    {
        var availableFrom = await _bookingService.VehicleAvailableFromAsync(vehicleId);

        return availableFrom.ToString("dd-MMM-yyyy");
    }

    [HttpGet("bookingHistoryByUser/{userId}")]
    public async Task<List<BookingResponseFull>> GetBookingHistoryByUser(Guid userId)
    {
        var bookingHistory = await _bookingService.GetBookingHistoryByUser(userId);

        return _mapper.Map<List<BookingResponseFull>>(bookingHistory);
    }

    [HttpGet("activeBookingByUser/{userId}")]
    public async Task<List<BookingResponseFull>> GetActiveBookingsByUser(Guid userId)
    {
        var activeBooking = await _bookingService.GetActiveBookingsByUser(userId);

        return _mapper.Map<List<BookingResponseFull>>(activeBooking);
    }

    [HttpGet("getReservationTypes")]
    [AllowAnonymous]
    public List<string> GetReservationTypes()
    {
        return _vehicleService.GetEnumToString(typeof(ReservationTypeApi));
    }
}