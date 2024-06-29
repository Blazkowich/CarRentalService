using CarRental.Service.Mapper.DTO.Enum;

namespace CarRental.Service.Mapper.DTO.Response;

public class VehicleResponseFull
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public VehicleTypeEnumApi Type { get; set; }

    public ReservationTypeApi ReservationType { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }
}
