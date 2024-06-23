using CarRental.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Context;

public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {
    }

    public DbSet<VehicleEntity> Vehicles { get; set; }

    public DbSet<BookingEntity> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
    }
}
