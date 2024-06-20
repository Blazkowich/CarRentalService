using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Repositories.Interfaces;

namespace CarRental.DAL.Repositories;

internal class CustomerRepository(CarRentalDbContext context) :
    BaseRepository<Guid, CustomerEntity>(context), ICustomerRepository
{
    private readonly CarRentalDbContext _context = context;
}
