using CarRental.DAL.Common.BaseRepository;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Context.Entities.Enum;

namespace CarRental.DAL.Repositories.Interfaces;

public interface IVehicleRepository : IBaseRepository<Guid, VehicleEntity>
{
    Task<VehicleEntity> GetVehicleByNameAsync(string name, CancellationToken ct);
    Task<List<VehicleEntity>> GetVehiclesByTypeAsync(VehicleTypeDAL type, CancellationToken ct);

    Task<List<VehicleEntity>> GetVehiclesByReservationTypeAsync(ReservationTypeDAL type, CancellationToken ct = default);

    Task<List<VehicleEntity>> GetAllAvailableVehicles(CancellationToken ct = default);
}
