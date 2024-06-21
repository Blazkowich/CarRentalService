using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class PermissionsEntityConfig : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        SeedPermissions(builder);
    }

    private static void SeedPermissions(EntityTypeBuilder<PermissionEntity> modelBuilder)
    {
        modelBuilder.HasData(
            new PermissionEntity { Id = Guid.Parse("ac8c1ccb-871e-4802-b998-08e4a5d7f933"), Name = "None" },
            new PermissionEntity { Id = Guid.Parse("dce04e14-aac2-4fbf-b3f5-38bbfc05718a"), Name = "ReadOnly" },
            new PermissionEntity { Id = Guid.Parse("4a47bab0-da9f-465f-b2ce-95abefab4371"), Name = "ReadWrite" });
    }
}
