using CarRental.Auth.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Auth.DAL.Context;

public class CarRentalAuthDbContext(DbContextOptions<CarRentalAuthDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RolesEntity> Roles { get; set; }

    public DbSet<PermissionEntity> Permissions { get; set; }

    public DbSet<UserRolesEntity> UserRoles { get; set; }

    public DbSet<UserPermissionsEntity> UserPermissions { get; set; }

    public DbSet<RolePermissionsEntity> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalAuthDbContext).Assembly);
    }
}

