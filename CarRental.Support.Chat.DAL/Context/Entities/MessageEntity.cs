namespace CarRental.Support.Chat.DAL.Context.Entities;

public class MessageEntity
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime Timestamp { get; set; }

    public int UserId { get; set; }
}
