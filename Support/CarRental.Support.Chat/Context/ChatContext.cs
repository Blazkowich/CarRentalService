using CarRental.Support.Chat.Model;
using MongoDB.Driver;

namespace CarRental.Support.Chat.Context;

public class ChatContext
{
    private readonly IMongoDatabase _database;

    public ChatContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<ChatMessage> ChatMessages => _database.GetCollection<ChatMessage>("ChatMessages");
}
