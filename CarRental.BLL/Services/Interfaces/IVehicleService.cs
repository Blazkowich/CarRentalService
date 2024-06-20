using CarRental.BLL.Models;

namespace CarRental.BLL.Services.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetVehiclesAsync();
}
