using CarRental.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Context.EntitiesConfig;

public class BookingEntityConfig : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingDate)
                   .IsRequired();

        builder.Property(b => b.StartDate)
               .IsRequired();

        builder.Property(b => b.EndDate)
               .IsRequired();

        builder.Property(b => b.TotalPrice)
               .IsRequired();

        builder.HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Vehicle)
            .WithMany()
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
