using AutoMapper;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("booking")]
public class BookingController(IBookingService bookingService, IMapper mapper) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<BookingResponseFull>> GetAllBookings()
    {
        var bookingResponse = await _bookingService.GetAllBookingsAsync();
        return _mapper.Map<List<BookingResponseFull>>(bookingResponse);
    }
}