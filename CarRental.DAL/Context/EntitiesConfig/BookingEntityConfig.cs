using CarRental.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Context.EntitiesConfig;

public class BookingEntityConfig : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingDate).IsRequired();
        builder.Property(b => b.StartDate).IsRequired();
        builder.Property(b => b.EndDate).IsRequired();
        builder.Property(b => b.TotalPrice).IsRequired();

        builder.HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Vehicle)
            .WithMany()
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedBookings(builder);
    }

    private static void SeedBookings(EntityTypeBuilder<BookingEntity> builder)
    {
        var now = DateTime.UtcNow;

        builder.HasData(
            new BookingEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.Parse("ac100f97-6db1-42ba-b3ad-a0881b167e50"), // Admin
                VehicleId = Guid.Parse("ab7d682c-0547-4f64-b78f-b51f2e4cb57b"), // Toyota Camry
                BookingDate = now,
                StartDate = now.AddDays(1),
                EndDate = now.AddDays(7),
            },
            new BookingEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.Parse("3877311a-f26e-4913-b28c-79fb64dc92d9"), // User
                VehicleId = Guid.Parse("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b"), // Ford Mustang
                BookingDate = now,
                StartDate = now.AddDays(3),
                EndDate = now.AddDays(10),
            },
            new BookingEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.Parse("3877311a-f26e-4913-b28c-79fb64dc92d9"), // User
                VehicleId = Guid.Parse("a8ce79ff-12eb-4538-a376-421f717a5148"), // Toyota Sienna
                BookingDate = now,
                StartDate = now.AddDays(-15),
                EndDate = now.AddDays(-10),
            }
        );
    }
}
