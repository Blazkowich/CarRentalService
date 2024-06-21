using CarRental.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Context.EntitiesConfig;

public class CustomerEntityConfig : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(c => c.Address)
            .HasMaxLength(200);
        SeedCustomers(builder);
    }

    private static void SeedCustomers(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasData(
            new CustomerEntity
            {
                Id = Guid.Parse("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Admin Street"
            },
            new CustomerEntity
            {
                Id = Guid.Parse("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                FirstName = "Regular",
                LastName = "User",
                Email = "user@example.com",
                PhoneNumber = "0987654321",
                Address = "456 User Lane"
            }
        );
    }
}
