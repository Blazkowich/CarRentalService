using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarRental.Support.Chat.DAL.Context.Entities;

namespace CarRental.Support.Chat.DAL.Context.EntitiesConfig;

public class MessageEntityConfig : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.HasKey(b => b.Id);
    }
}
