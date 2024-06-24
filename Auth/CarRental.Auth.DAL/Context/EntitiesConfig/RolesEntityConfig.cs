using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class RolesEntityConfig : IEntityTypeConfiguration<RolesEntity>
{
    public void Configure(EntityTypeBuilder<RolesEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(u => u.RolePermissions)
            .WithOne(u => u.Role)
            .HasForeignKey(up => up.RoleId);

        SeedRoles(builder);
    }

    private static void SeedRoles(EntityTypeBuilder<RolesEntity> modelBuilder)
    {
        modelBuilder.HasData(
            new RolesEntity { Id = Guid.Parse("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc"), Name = "Admin" },
            new RolesEntity { Id = Guid.Parse("765f9e20-fb70-4837-8b22-5d280ad9d2d2"), Name = "Manager" },
            new RolesEntity { Id = Guid.Parse("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d"), Name = "User" },
            new RolesEntity { Id = Guid.Parse("6894d1ff-f40c-418e-b560-f6670e8b1e4e"), Name = "Guest" });
    }
}
