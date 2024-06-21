using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class RolePermissionsEntityConfig : IEntityTypeConfiguration<RolePermissionsEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
        builder.HasKey(up => new { up.RoleId, up.PermissionId });

        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);
        SeedRolePermissions(builder);
    }

    private static void SeedRolePermissions(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
        builder.HasData(
            new RolePermissionsEntity { RoleId = Guid.Parse("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc"), PermissionId = Guid.Parse("4a47bab0-da9f-465f-b2ce-95abefab4371") },
            new RolePermissionsEntity { RoleId = Guid.Parse("765f9e20-fb70-4837-8b22-5d280ad9d2d2"), PermissionId = Guid.Parse("ac8c1ccb-871e-4802-b998-08e4a5d7f933") },
            new RolePermissionsEntity { RoleId = Guid.Parse("765f9e20-fb70-4837-8b22-5d280ad9d2d2"), PermissionId = Guid.Parse("dce04e14-aac2-4fbf-b3f5-38bbfc05718a") },
            new RolePermissionsEntity { RoleId = Guid.Parse("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d"), PermissionId = Guid.Parse("dce04e14-aac2-4fbf-b3f5-38bbfc05718a") },
            new RolePermissionsEntity { RoleId = Guid.Parse("6894d1ff-f40c-418e-b560-f6670e8b1e4e"), PermissionId = Guid.Parse("ac8c1ccb-871e-4802-b998-08e4a5d7f933") });
    }
}


