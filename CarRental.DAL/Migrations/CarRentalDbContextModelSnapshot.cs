﻿// <auto-generated />
using System;
using CarRental.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental.DAL.Migrations
{
    [DbContext(typeof(CarRentalDbContext))]
    partial class CarRentalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRental.DAL.Context.Entities.BookingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BookingCondition")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("948ee4ec-195b-4f96-8df2-34f6dcd26741"),
                            BookingCondition = 0,
                            BookingDate = new DateTime(2024, 6, 22, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            CustomerId = new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                            EndDate = new DateTime(2024, 6, 29, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            StartDate = new DateTime(2024, 6, 23, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            TotalPrice = 0.0,
                            VehicleId = new Guid("ab7d682c-0547-4f64-b78f-b51f2e4cb57b")
                        },
                        new
                        {
                            Id = new Guid("9ee0a7ed-0106-4fcb-b555-a39019072685"),
                            BookingCondition = 0,
                            BookingDate = new DateTime(2024, 6, 22, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            CustomerId = new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                            EndDate = new DateTime(2024, 7, 2, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            StartDate = new DateTime(2024, 6, 25, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            TotalPrice = 0.0,
                            VehicleId = new Guid("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b")
                        },
                        new
                        {
                            Id = new Guid("ad0f0756-b575-4609-8093-aecc763f3975"),
                            BookingCondition = 3,
                            BookingDate = new DateTime(2024, 6, 22, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            CustomerId = new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                            EndDate = new DateTime(2024, 6, 12, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            StartDate = new DateTime(2024, 6, 7, 13, 44, 28, 514, DateTimeKind.Utc).AddTicks(1575),
                            TotalPrice = 0.0,
                            VehicleId = new Guid("a8ce79ff-12eb-4538-a376-421f717a5148")
                        });
                });

            modelBuilder.Entity("CarRental.DAL.Context.Entities.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                            Address = "123 Admin Street",
                            Email = "admin@example.com",
                            FirstName = "Admin",
                            LastName = "User",
                            PhoneNumber = "1234567890"
                        },
                        new
                        {
                            Id = new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                            Address = "456 User Lane",
                            Email = "user@example.com",
                            FirstName = "Regular",
                            LastName = "User",
                            PhoneNumber = "0987654321"
                        });
                });

            modelBuilder.Entity("CarRental.DAL.Context.Entities.VehicleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ReservationType")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ab7d682c-0547-4f64-b78f-b51f2e4cb57b"),
                            Description = "Mid-size sedan",
                            ImageUrl = "https://akm-img-a-in.tosshub.com/indiatoday/images/story/202201/2022_Toyota_Camry_Hybrid-_Exte.jpg",
                            Name = "Toyota Camry",
                            Price = 50.0,
                            ReservationType = 0,
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b"),
                            Description = "Sports coupe",
                            ImageUrl = "https://cdn.motor1.com/images/mgl/7ZZObp/s3/ford-mustang-2023.jpg",
                            Name = "Ford Mustang",
                            Price = 80.0,
                            ReservationType = 0,
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("a8ce79ff-12eb-4538-a376-421f717a5148"),
                            Description = "Family van",
                            ImageUrl = "https://cdn.motor1.com/images/mgl/kX72B/s1/2021-toyota-sienna.webp",
                            Name = "Toyota Sienna",
                            Price = 65.0,
                            ReservationType = 1,
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("95594807-ed1b-489c-af41-89b0cc1c7b6b"),
                            Description = "Compact hatchback",
                            ImageUrl = "https://avatars.mds.yandex.net/get-verba/997355/2a0000017fb5dcbb8166c3139f13d65f667f/cattouchret",
                            Name = "Ford Focus",
                            Price = 55.0,
                            ReservationType = 1,
                            Type = 3
                        },
                        new
                        {
                            Id = new Guid("5fc7a66f-2b55-4f2e-a439-426d89b081c7"),
                            Description = "SUV",
                            ImageUrl = "https://s.auto.drom.ru/i24282/c/photos/fullsize/chevrolet/tahoe/chevrolet_tahoe_1143697.jpg",
                            Name = "Chevrolet Tahoe",
                            Price = 90.0,
                            ReservationType = 1,
                            Type = 4
                        },
                        new
                        {
                            Id = new Guid("029d2a5e-730b-4154-bdf3-7e6d758cbe6d"),
                            Description = "Off-road SUV",
                            ImageUrl = "https://media.ed.edmunds-media.com/jeep/wrangler/2024/oem/2024_jeep_wrangler_convertible-suv_rubicon-392-final-edition_fq_oem_1_1600.jpg",
                            Name = "Jeep Wrangler",
                            Price = 85.0,
                            ReservationType = 1,
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("30c5dd2c-bafe-48be-98a1-09c5cf349a65"),
                            Description = "Minivan",
                            ImageUrl = "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/wp-content/uploads/2016/06/2016-Honda-Odyssey-Touring-Elite-101.jpg",
                            Name = "Honda Odyssey",
                            Price = 70.0,
                            ReservationType = 1,
                            Type = 6
                        },
                        new
                        {
                            Id = new Guid("2806518a-a956-4936-aefd-1fe6e74aa987"),
                            Description = "Luxury Stretch SUV",
                            ImageUrl = "https://images.squarespace-cdn.com/content/v1/59a18fe46b8f5be647f23512/1632108265164-O3P64AQ7IVHDAP5PFN9Q/VIP+Limo+Service+Cadillac+Escalade+SUV+Stretch+Limo+Exterior+09.jpg?format=1500w",
                            Name = "Cadillac Escalade",
                            Price = 150.0,
                            ReservationType = 1,
                            Type = 7
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
