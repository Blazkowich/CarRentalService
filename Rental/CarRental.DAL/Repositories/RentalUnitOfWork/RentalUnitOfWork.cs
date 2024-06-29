using CarRental.DAL.Common.BaseUnitOfWork;
using CarRental.DAL.Context;
using CarRental.DAL.Repositories.Interfaces;

namespace CarRental.DAL.Repositories.RentalUnitOfWork;

public class RentalUnitOfWork : BaseUnitOfWork, IRentalUnitOfWork
{
    private readonly CarRentalDbContext _context;

    public RentalUnitOfWork(
        CarRentalDbContext context,
        IVehicleRepository vehiclesRepository,
        IBookingRepository bookingsRepository
        ) : base(context)
    {
        _context = context;
        VehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        BookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
    }

    public IVehicleRepository VehiclesRepository { get; private set; }

    public IBookingRepository BookingsRepository { get; private set; }
}
