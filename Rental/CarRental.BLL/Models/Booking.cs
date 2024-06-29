using CarRental.BLL.Models.Enum;

namespace CarRental.BLL.Models;

public class Booking
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double TotalPrice { get; set; }

    public BookingTypeBLL BookingCondition { get; set; }
}
