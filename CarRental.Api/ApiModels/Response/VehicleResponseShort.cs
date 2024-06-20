using CarRental.Api.ApiModels.Enum;

namespace CarRental.Api.ApiModels.Response;

public class VehicleResponseShort
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public VehicleTypeEnumApi Type { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }
}
