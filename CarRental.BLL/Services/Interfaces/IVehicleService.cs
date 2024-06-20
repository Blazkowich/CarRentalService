using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.DAL.Context.Entities;

namespace CarRental.BLL.Services.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetVehiclesAsync();

    Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);

    Task<Vehicle> GetVehicleByNameAsync(string name);

    Task<List<Vehicle>> GetVehiclesByTypeAsync(VehicleTypeBLL type);

    Task<Guid> AddVehicleAsync(Vehicle vehicle);

    Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);

    Task DeleteVehicleAsync(Guid vehicleId);
}
