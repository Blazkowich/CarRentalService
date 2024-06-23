using AutoMapper;
using CarRental.Api.ApiModels.Enum;
using CarRental.Api.ApiModels.Request;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;

namespace CarRental.Api.ApiAutoMapper;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        #region Vehicle
        CreateMap<VehicleRequest, Vehicle>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ReservationType, opt => opt.MapFrom(src => src.ReservationType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<Vehicle, VehicleResponseFull>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ReservationType, opt => opt.MapFrom(src => src.ReservationType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<VehicleTypeEnumApi, VehicleTypeBLL>();
        CreateMap<VehicleTypeBLL, VehicleTypeEnumApi>();

        CreateMap<ReservationTypeApi, ReservationTypeBLL>();
        CreateMap<ReservationTypeBLL, ReservationTypeApi>();
        #endregion

        #region Booking
        CreateMap<BookingRequest, Booking>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.BookingCondition, opt => opt.MapFrom(src => src.BookingCondition));

        CreateMap<Booking, BookingResponseFull>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.BookingCondition, opt => opt.MapFrom(src => src.BookingCondition));

        CreateMap<BookingTypeApi, BookingTypeBLL>();
        CreateMap<BookingTypeBLL, BookingTypeApi>();
        #endregion
    }
}

