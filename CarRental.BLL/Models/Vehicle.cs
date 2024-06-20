using CarRental.BLL.Models.Enum;

namespace CarRental.BLL.Models;

public class Vehicle
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public VehicleTypeBLL Type { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }
}
