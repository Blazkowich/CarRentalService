using CarRental.Support.Chat.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Support.Chat.DAL.Context;

public class CarRentalChatDbContext(DbContextOptions<CarRentalChatDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalChatDbContext).Assembly);
    }
}
