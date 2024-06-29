using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;

namespace CarRental.Service.Mapper.Services.Interfaces
{
    public interface IVehicleMapped
    {
        Task<List<VehicleResponseFull>> GetAllVehiclesAsync();
        Task<VehicleResponseFull> GetVehicleByIdAsync(Guid vehicleId);
        Task<VehicleResponseFull> GetVehicleByNameAsync(string name);
        Task<List<VehicleResponseFull>> GetVehiclesByTypeAsync(VehicleTypeEnumApi type);
        Task<List<VehicleResponseFull>> GetAllAvailableVehiclesAsync();
        Task<List<VehicleResponseFull>> GetVehiclesByReservationTypeAsync(ReservationTypeApi reservationType);
        Task<VehicleResponseFull> AddVehicleAsync(VehicleRequest vehicle);
        Task<VehicleResponseFull> UpdateVehicleAsync(VehicleRequest vehicle);
        Task DeleteVehicleAsync(VehicleRequest vehicle);
        List<string> GetVehicleTypes();
    }
}
