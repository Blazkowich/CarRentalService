using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Support.Chat.Model;

public class ChatMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Sender { get; set; }

    public Guid SenderId { get; set; }

    public string Receiver { get; set; }

    public string Message { get; set; }

    public DateTime Timestamp { get; set; }
}
