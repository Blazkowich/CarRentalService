using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.DAL.Context.Entities;

public class VehicleEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public VehicleTypeDAL Type { get; set; }

    public ReservationTypeDAL ReservationType { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }
}
