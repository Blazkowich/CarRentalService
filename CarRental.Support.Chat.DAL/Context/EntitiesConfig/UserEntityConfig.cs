using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarRental.Support.Chat.DAL.Context.Entities;

namespace CarRental.Support.Chat.DAL.Context.EntitiesConfig;

public class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(b => b.Id);
    }
}
