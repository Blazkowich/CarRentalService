using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;
using CarRental.Service.Mapper.Services.Interfaces;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services
{
    internal class BookingMapped : IBookingMapped
    {
        private readonly IBookingService _bookingService;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public BookingMapped(IBookingService bookingService, IVehicleService vehicleService, IMapper mapper)
        {
            _bookingService = bookingService;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<List<BookingResponseFull>> GetAllBookingsAsync()
        {
            var bookingResponse = await _bookingService.GetAllBookingsAsync();
            return _mapper.Map<List<BookingResponseFull>>(bookingResponse);
        }

        public async Task<List<BookingResponseFull>> GetBookingsByConditionAsync(BookingTypeApi bookingCondition)
        {
            var getBookingByCondition = await _bookingService
                .GetBookingsByConditionAsync(_mapper.Map<BookingTypeBLL>(bookingCondition));

            return _mapper.Map<List<BookingResponseFull>>(getBookingByCondition);
        }

        public async Task<BookingResponseFull> ReserveVehicleAsync(Guid vehicleId, DateTime startDate, int duration, ClaimsPrincipal user)
        {
            var reserveVehicle = await _bookingService.ReserveVehicleAsync(vehicleId, startDate, duration, user);

            return _mapper.Map<BookingResponseFull>(reserveVehicle);
        }

        public async Task<BookingResponseFull> BookVehicleAsync(BookingRequest bookingRequest, ClaimsPrincipal user)
        {
            var bookVehicle = await _bookingService.BookVehicleAsync(bookingRequest.VehicleId, bookingRequest.Duration, user);

            return _mapper.Map<BookingResponseFull>(bookVehicle);
        }

        public async Task<BookingResponseFull> CancelBookingOrReservationAsync(BookingRequest bookingRequest)
        {
            var cancel = await _bookingService.CancelBookingAsync(_mapper.Map<Booking>(bookingRequest));

            return _mapper.Map<BookingResponseFull>(cancel);
        }

        public async Task<string> VehicleAvailableFromAsync(Guid vehicleId)
        {
            var availableFrom = await _bookingService.VehicleAvailableFromAsync(vehicleId);

            return availableFrom.ToString("dd-MMM-yyyy");
        }

        public async Task<List<BookingResponseFull>> GetBookingHistoryByUserAsync(Guid userId)
        {
            var bookingHistory = await _bookingService.GetBookingHistoryByUser(userId);

            return _mapper.Map<List<BookingResponseFull>>(bookingHistory);
        }

        public async Task<List<BookingResponseFull>> GetActiveBookingsByUserAsync(Guid userId)
        {
            var activeBooking = await _bookingService.GetActiveBookingsByUser(userId);

            return _mapper.Map<List<BookingResponseFull>>(activeBooking);
        }

        public List<string> GetReservationTypes()
        {
            return _vehicleService.GetEnumToString(typeof(ReservationTypeApi));
        }
    }
}
