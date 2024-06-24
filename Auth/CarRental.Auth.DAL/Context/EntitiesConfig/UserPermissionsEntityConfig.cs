using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class UserPermissionEntityConfig : IEntityTypeConfiguration<UserPermissionsEntity>
{
    public void Configure(EntityTypeBuilder<UserPermissionsEntity> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PermissionId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.Permissions)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Permission)
            .WithMany(p => p.UserPermissions)
            .HasForeignKey(up => up.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedUserPermissions(builder);
    }

    private static void SeedUserPermissions(EntityTypeBuilder<UserPermissionsEntity> builder)
    {
        builder.HasData(
            new UserPermissionsEntity
            {
                UserId = Guid.Parse("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                PermissionId = Guid.Parse("4a47bab0-da9f-465f-b2ce-95abefab4371"),
            },
            new UserPermissionsEntity
            {
                UserId = Guid.Parse("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                PermissionId = Guid.Parse("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"),
            });
    }
}


