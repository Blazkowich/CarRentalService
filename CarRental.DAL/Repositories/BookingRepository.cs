using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Repositories.Interfaces;

namespace CarRental.DAL.Repositories;

internal class BookingRepository(CarRentalDbContext context) :
    BaseRepository<Guid, BookingEntity>(context), IBookingRepository
{
    private readonly CarRentalDbContext _context = context;
}
