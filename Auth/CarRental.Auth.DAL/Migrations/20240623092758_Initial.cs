using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.Auth.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4a47bab0-da9f-465f-b2ce-95abefab4371"), "ReadWrite" },
                    { new Guid("ac8c1ccb-871e-4802-b998-08e4a5d7f933"), "None" },
                    { new Guid("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"), "ReadOnly" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc"), "Admin" },
                    { new Guid("6894d1ff-f40c-418e-b560-f6670e8b1e4e"), "Guest" },
                    { new Guid("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d"), "User" },
                    { new Guid("765f9e20-fb70-4837-8b22-5d280ad9d2d2"), "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"), "456 User Lane", "a7x.otto@gmail.com", "Regular", "User", "user", new byte[] { 139, 155, 144, 13, 123, 180, 160, 98, 71, 169, 235, 146, 178, 157, 35, 65, 84, 220, 59, 244, 154, 207, 76, 23, 166, 205, 196, 159, 7, 41, 28, 81 }, new byte[] { 19, 25, 122, 208, 169, 250, 199, 184, 179, 59, 220, 78, 161, 31, 115, 91, 121, 6, 137, 150, 93, 141, 163, 194, 18, 196, 169, 117, 249, 66, 4, 220, 27, 20, 68, 75, 184, 188, 44, 61, 119, 198, 66, 29, 48, 9, 160, 234, 33, 224, 165, 72, 71, 37, 91, 149, 23, 87, 126, 175, 213, 142, 81, 97 }, "0987654321", "", new DateTime(2024, 6, 23, 9, 27, 57, 893, DateTimeKind.Utc).AddTicks(3571), new DateTime(2024, 6, 24, 9, 27, 57, 893, DateTimeKind.Utc).AddTicks(3574) },
                    { new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"), "123 Admin Street", "admin@admin.com", "Admin", "Adminov", "admin", new byte[] { 183, 37, 153, 86, 122, 13, 165, 152, 199, 114, 118, 112, 168, 181, 113, 252, 0, 43, 136, 49, 42, 85, 145, 58, 158, 175, 139, 251, 112, 229, 20, 28 }, new byte[] { 2, 236, 93, 133, 227, 180, 250, 105, 10, 126, 192, 88, 38, 197, 17, 207, 33, 86, 87, 148, 40, 18, 88, 124, 67, 236, 44, 55, 57, 176, 246, 215, 192, 82, 33, 195, 104, 26, 207, 196, 88, 196, 59, 94, 52, 18, 88, 119, 29, 184, 91, 54, 18, 244, 209, 215, 161, 185, 173, 109, 90, 65, 147, 120 }, "1234567890", "", new DateTime(2024, 6, 23, 9, 27, 57, 893, DateTimeKind.Utc).AddTicks(3556), new DateTime(2024, 6, 24, 9, 27, 57, 893, DateTimeKind.Utc).AddTicks(3566) }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("4a47bab0-da9f-465f-b2ce-95abefab4371"), new Guid("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc") },
                    { new Guid("ac8c1ccb-871e-4802-b998-08e4a5d7f933"), new Guid("6894d1ff-f40c-418e-b560-f6670e8b1e4e") },
                    { new Guid("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"), new Guid("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d") },
                    { new Guid("ac8c1ccb-871e-4802-b998-08e4a5d7f933"), new Guid("765f9e20-fb70-4837-8b22-5d280ad9d2d2") },
                    { new Guid("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"), new Guid("765f9e20-fb70-4837-8b22-5d280ad9d2d2") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[,]
                {
                    { new Guid("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"), new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9") },
                    { new Guid("4a47bab0-da9f-465f-b2ce-95abefab4371"), new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d"), new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9") },
                    { new Guid("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc"), new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
