using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(u => u.Roles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasMany(u => u.Permissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId);

        SeedUsers(builder);
    }

    private static void SeedUsers(EntityTypeBuilder<UserEntity> builder)
    {
        CreatePasswordHash("admin", out byte[] adminPasswordHash, out byte[] adminPasswordSalt);
        CreatePasswordHash("password", out byte[] userPasswordHash, out byte[] userPasswordSalt);

        builder.HasData(
            new UserEntity
            {
                Id = Guid.Parse("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                Name = "admin",
                FirstName = "Admin",
                LastName = "Adminov",
                Email = "admin@admin.com",
                PhoneNumber = "1234567890",
                Address = "123 Admin Street",
                PasswordHash = adminPasswordHash,
                PasswordSalt = adminPasswordSalt,
                TokenExpires = DateTime.UtcNow.AddDays(1),
            },
            new UserEntity
            {
                Id = Guid.Parse("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                Name = "user",
                FirstName = "Regular",
                LastName = "User",
                Email = "a7x.otto@gmail.com",
                PhoneNumber = "0987654321",
                Address = "456 User Lane",
                PasswordHash = userPasswordHash,
                PasswordSalt = userPasswordSalt,
                TokenExpires = DateTime.UtcNow.AddDays(1),
            });
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}

