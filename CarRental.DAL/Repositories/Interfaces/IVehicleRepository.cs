using CarRental.DAL.Common.Repositories.Interfaces;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.DAL.Repositories.Interfaces;

public interface IVehicleRepository : IBaseRepository<Guid, VehicleEntity>
{
    Task<VehicleEntity> GetVehicleByNameAsync(string name, CancellationToken ct);
    Task<List<VehicleEntity>> GetVehiclesByTypeAsync(VehicleTypeDAL type, CancellationToken ct);

    Task<List<VehicleEntity>> GetAllAvailableVehicles();
}
