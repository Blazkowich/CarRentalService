using CarRental.Service.Mapper.DTO.Enum;

namespace CarRental.Service.Mapper.DTO.Request;

public class BookingRequest
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double TotalPrice { get; set; }

    public BookingTypeApi BookingCondition { get; set; }

    public int Duration { get; set; }
}
