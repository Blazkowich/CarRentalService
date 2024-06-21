using CarRental.DAL.Common.Repositories;
using CarRental.DAL.Context;
using CarRental.DAL.Context.Entities;
using CarRental.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories;

internal class BookingRepository(CarRentalDbContext context) :
    BaseRepository<Guid, BookingEntity>(context), IBookingRepository
{
    private readonly CarRentalDbContext _context = context;

    public async Task<List<BookingEntity>> GetOverduedBookingsAsync()
    {
        var overdueBookings = await _context.Bookings
            .Where(b => b.EndDate <= DateTime.UtcNow)
            .Include(b => b.Vehicle)
            .ToListAsync();

        return overdueBookings;
    }
}
