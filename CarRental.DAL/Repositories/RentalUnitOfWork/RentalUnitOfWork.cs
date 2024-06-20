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
        ICustomerRepository customersRepository,
        IBookingRepository bookingsRepository
        ) : base(context)
    {
        _context = context;
        VehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        CustomersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
        BookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
    }

    public IVehicleRepository VehiclesRepository { get; private set; }

    public ICustomerRepository CustomersRepository { get; private set; }

    public IBookingRepository BookingsRepository { get; private set; }
}
