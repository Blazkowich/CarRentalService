using CarRental.Auth.BLL.Models;
using CarRental.Support.Chat.Context;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using MongoDB.Driver;

namespace CarRental.Support.Chat.Services;

public class ChatMessageService(ChatContext context) : IChatMessageService
{
    private readonly IMongoCollection<ChatMessage> _chatMessages = context.ChatMessages;

    public async Task SaveMessage(ChatMessage message)
    {
        await _chatMessages.InsertOneAsync(message);
    }

    public async Task<List<ChatMessage>> GetMessages(Guid userId)
    {
        return await _chatMessages.Find(m => m.Receiver == "Admin" && m.SenderId == userId).ToListAsync();
    }

    public async Task<List<ChatMessage>> GetAllMessages()
    {
        return await _chatMessages.Find(m => m.Receiver == "Admin").ToListAsync();
    }
}
