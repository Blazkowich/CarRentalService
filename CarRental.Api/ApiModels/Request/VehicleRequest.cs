using CarRental.Api.ApiModels.Enum;

namespace CarRental.Api.ApiModels.Request;

public class VehicleRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public VehicleTypeEnumApi Type { get; set; }

    public ReservationTypeApi ReservationType { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }
}
