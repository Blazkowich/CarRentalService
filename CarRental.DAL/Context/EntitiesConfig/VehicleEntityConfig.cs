using CarRental.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Context.EntitiesConfig;

public class VehicleEntityConfig : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(v => v.Description)
               .HasMaxLength(500);

        builder.Property(v => v.Type)
               .IsRequired();

        builder.Property(v => v.Price)
               .IsRequired();

        SeedVehicles(builder);
    }

    private static void SeedVehicles(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasData(
            new VehicleEntity
            {
                Id = Guid.Parse("ab7d682c-0547-4f64-b78f-b51f2e4cb57b"),
                Name = "Toyota Camry",
                Description = "Mid-size sedan",
                Type = VehicleTypeEnum.Sedan,
                Price = 50.00,
                ImageUrl = "https://akm-img-a-in.tosshub.com/indiatoday/images/story/202201/2022_Toyota_Camry_Hybrid-_Exte.jpg"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b"),
                Name = "Ford Mustang",
                Description = "Sports coupe",
                Type = VehicleTypeEnum.Coupe,
                Price = 80.00,
                ImageUrl = "https://cdn.motor1.com/images/mgl/7ZZObp/s3/ford-mustang-2023.jpg"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("a8ce79ff-12eb-4538-a376-421f717a5148"),
                Name = "Toyota Sienna",
                Description = "Family van",
                Type = VehicleTypeEnum.Van,
                Price = 65.00,
                ImageUrl = "https://cdn.motor1.com/images/mgl/kX72B/s1/2021-toyota-sienna.webp"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("95594807-ed1b-489c-af41-89b0cc1c7b6b"),
                Name = "Ford Focus",
                Description = "Compact hatchback",
                Type = VehicleTypeEnum.HatchBack,
                Price = 55.00,
                ImageUrl = "https://avatars.mds.yandex.net/get-verba/997355/2a0000017fb5dcbb8166c3139f13d65f667f/cattouchret"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("5fc7a66f-2b55-4f2e-a439-426d89b081c7"),
                Name = "Chevrolet Tahoe",
                Description = "SUV",
                Type = VehicleTypeEnum.SUV,
                Price = 90.00,
                ImageUrl = "https://s.auto.drom.ru/i24282/c/photos/fullsize/chevrolet/tahoe/chevrolet_tahoe_1143697.jpg"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("029d2a5e-730b-4154-bdf3-7e6d758cbe6d"),
                Name = "Jeep Wrangler",
                Description = "Off-road SUV",
                Type = VehicleTypeEnum.Jeep,
                Price = 85.00,
                ImageUrl = "https://media.ed.edmunds-media.com/jeep/wrangler/2024/oem/2024_jeep_wrangler_convertible-suv_rubicon-392-final-edition_fq_oem_1_1600.jpg"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("30c5dd2c-bafe-48be-98a1-09c5cf349a65"),
                Name = "Honda Odyssey",
                Description = "Minivan",
                Type = VehicleTypeEnum.MiniVan,
                Price = 70.00,
                ImageUrl = "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/wp-content/uploads/2016/06/2016-Honda-Odyssey-Touring-Elite-101.jpg"
            },
            new VehicleEntity
            {
                Id = Guid.Parse("2806518a-a956-4936-aefd-1fe6e74aa987"),
                Name = "Cadillac Escalade",
                Description = "Luxury Stretch SUV",
                Type = VehicleTypeEnum.Limousine,
                Price = 150.00,
                ImageUrl = "https://images.squarespace-cdn.com/content/v1/59a18fe46b8f5be647f23512/1632108265164-O3P64AQ7IVHDAP5PFN9Q/VIP+Limo+Service+Cadillac+Escalade+SUV+Stretch+Limo+Exterior+09.jpg?format=1500w"
            }
        );
    }
}
