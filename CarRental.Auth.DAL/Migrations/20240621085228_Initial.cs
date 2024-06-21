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
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"), "user", new byte[] { 11, 91, 53, 170, 250, 35, 125, 132, 65, 17, 196, 128, 96, 115, 12, 106, 9, 102, 16, 203, 43, 214, 92, 186, 183, 50, 56, 82, 127, 72, 179, 200 }, new byte[] { 154, 110, 32, 11, 239, 76, 122, 39, 249, 96, 21, 35, 98, 195, 203, 29, 89, 240, 213, 114, 233, 46, 41, 189, 66, 54, 81, 72, 233, 18, 184, 146, 253, 94, 150, 3, 166, 218, 64, 100, 209, 21, 216, 3, 13, 148, 195, 234, 79, 109, 66, 174, 3, 69, 255, 153, 217, 28, 158, 229, 65, 101, 138, 218 }, "", new DateTime(2024, 6, 21, 8, 52, 27, 698, DateTimeKind.Utc).AddTicks(1241), new DateTime(2024, 6, 22, 8, 52, 27, 698, DateTimeKind.Utc).AddTicks(1243) },
                    { new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"), "admin", new byte[] { 181, 84, 124, 37, 29, 213, 104, 49, 23, 119, 81, 223, 212, 186, 99, 229, 82, 121, 173, 94, 50, 22, 36, 133, 97, 212, 90, 66, 218, 141, 26, 38 }, new byte[] { 57, 30, 177, 188, 87, 211, 11, 164, 251, 176, 81, 15, 148, 114, 195, 20, 168, 124, 158, 140, 37, 230, 116, 209, 134, 177, 144, 51, 140, 1, 248, 214, 75, 12, 254, 49, 231, 228, 72, 9, 114, 252, 166, 89, 75, 75, 154, 157, 128, 185, 157, 206, 198, 81, 134, 15, 167, 243, 45, 16, 182, 68, 49, 82 }, "", new DateTime(2024, 6, 21, 8, 52, 27, 698, DateTimeKind.Utc).AddTicks(1229), new DateTime(2024, 6, 22, 8, 52, 27, 698, DateTimeKind.Utc).AddTicks(1235) }
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
