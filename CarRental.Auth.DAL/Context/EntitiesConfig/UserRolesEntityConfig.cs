using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Auth.DAL.Context.EntitiesConfig;

internal class UserRolesEntityConfig : IEntityTypeConfiguration<UserRolesEntity>
{
    public void Configure(EntityTypeBuilder<UserRolesEntity> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(us => us.User)
            .WithMany(ur => ur.Roles)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedUserRoles(builder);
    }

    private static void SeedUserRoles(EntityTypeBuilder<UserRolesEntity> builder)
    {
        builder.HasData(
            new UserRolesEntity
            {
                UserId = new Guid("ac100f97-6db1-42ba-b3ad-a0881b167e50"),
                RoleId = new Guid("529e960f-79c9-4e25-b3ef-a5ce8cbb42bc"),
            },
            new UserRolesEntity
            {
                UserId = new Guid("3877311a-f26e-4913-b28c-79fb64dc92d9"),
                RoleId = new Guid("7350e0d2-2a91-42ce-bfe8-d882eff2ce3d"),
            });
    }
}

