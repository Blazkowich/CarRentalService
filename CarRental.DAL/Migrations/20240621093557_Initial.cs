using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"), "456 User Lane", "user@example.com", "Regular", "User", "0987654321" },
                    { new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"), "123 Admin Street", "admin@example.com", "Admin", "User", "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { new Guid("029d2a5e-730b-4154-bdf3-7e6d758cbe6d"), "Off-road SUV", "https://media.ed.edmunds-media.com/jeep/wrangler/2024/oem/2024_jeep_wrangler_convertible-suv_rubicon-392-final-edition_fq_oem_1_1600.jpg", "Jeep Wrangler", 85.0, 5 },
                    { new Guid("2806518a-a956-4936-aefd-1fe6e74aa987"), "Luxury Stretch SUV", "https://images.squarespace-cdn.com/content/v1/59a18fe46b8f5be647f23512/1632108265164-O3P64AQ7IVHDAP5PFN9Q/VIP+Limo+Service+Cadillac+Escalade+SUV+Stretch+Limo+Exterior+09.jpg?format=1500w", "Cadillac Escalade", 150.0, 7 },
                    { new Guid("30c5dd2c-bafe-48be-98a1-09c5cf349a65"), "Minivan", "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/wp-content/uploads/2016/06/2016-Honda-Odyssey-Touring-Elite-101.jpg", "Honda Odyssey", 70.0, 6 },
                    { new Guid("5fc7a66f-2b55-4f2e-a439-426d89b081c7"), "SUV", "https://s.auto.drom.ru/i24282/c/photos/fullsize/chevrolet/tahoe/chevrolet_tahoe_1143697.jpg", "Chevrolet Tahoe", 90.0, 4 },
                    { new Guid("95594807-ed1b-489c-af41-89b0cc1c7b6b"), "Compact hatchback", "https://avatars.mds.yandex.net/get-verba/997355/2a0000017fb5dcbb8166c3139f13d65f667f/cattouchret", "Ford Focus", 55.0, 3 },
                    { new Guid("a8ce79ff-12eb-4538-a376-421f717a5148"), "Family van", "https://cdn.motor1.com/images/mgl/kX72B/s1/2021-toyota-sienna.webp", "Toyota Sienna", 65.0, 2 },
                    { new Guid("ab7d682c-0547-4f64-b78f-b51f2e4cb57b"), "Mid-size sedan", "https://akm-img-a-in.tosshub.com/indiatoday/images/story/202201/2022_Toyota_Camry_Hybrid-_Exte.jpg", "Toyota Camry", 50.0, 0 },
                    { new Guid("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b"), "Sports coupe", "https://cdn.motor1.com/images/mgl/7ZZObp/s3/ford-mustang-2023.jpg", "Ford Mustang", 80.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CustomerId", "EndDate", "StartDate", "TotalPrice", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("ce634062-7b50-4f4a-9eef-151176df31ec"), new DateTime(2024, 6, 21, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8670), new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"), new DateTime(2024, 6, 28, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8677), new DateTime(2024, 6, 22, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8672), 350.0, new Guid("ab7d682c-0547-4f64-b78f-b51f2e4cb57b") },
                    { new Guid("d96341ea-4cb2-4038-8faa-d857db9586e7"), new DateTime(2024, 6, 21, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8681), new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"), new DateTime(2024, 7, 1, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8682), new DateTime(2024, 6, 24, 9, 35, 56, 944, DateTimeKind.Utc).AddTicks(8681), 560.0, new Guid("eaf81fda-e22b-4cb9-8d49-8bba62a7f83b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VehicleId",
                table: "Bookings",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
