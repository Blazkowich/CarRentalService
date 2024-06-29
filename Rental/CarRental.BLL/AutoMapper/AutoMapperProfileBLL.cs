using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.BLL.AutoMapper;

public class AutomapperProfileBLL : Profile
{
    public AutomapperProfileBLL()
    {
        #region Vehicle
        CreateMap<Vehicle, VehicleEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ReservationType, opt => opt.MapFrom(src => src.ReservationType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<VehicleEntity, Vehicle>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ReservationType, opt => opt.MapFrom(src => src.ReservationType))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<VehicleTypeBLL, VehicleTypeDAL>();
        CreateMap<VehicleTypeDAL, VehicleTypeBLL>();

        CreateMap<ReservationTypeBLL, ReservationTypeDAL>();
        CreateMap<ReservationTypeDAL, ReservationTypeDAL>();
        #endregion

        #region Booking
        CreateMap<Booking, BookingEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.BookingCondition, opt => opt.MapFrom(src => src.BookingCondition));

        CreateMap<BookingEntity, Booking>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleId))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.BookingCondition, opt => opt.MapFrom(src => src.BookingCondition));

        CreateMap<BookingTypeBLL, BookingTypeDAL>();
        CreateMap<BookingTypeDAL, BookingTypeBLL>();
        #endregion
    }
}