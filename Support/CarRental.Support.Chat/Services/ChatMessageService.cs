using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Support.Chat.Context;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using MongoDB.Driver;

namespace CarRental.Support.Chat.Services;

public class ChatMessageService(ChatContext context, IUserService userService) : IChatMessageService
{
    private readonly IMongoCollection<ChatMessage> _chatMessages = context.ChatMessages;
    private readonly IUserService _userService = userService;

    public async Task SaveMessage(ChatMessage message)
    {
        await _chatMessages.InsertOneAsync(message);
    }

    public async Task<List<ChatMessage>> GetMessages(string userId)
    {
        var result = await _chatMessages
            .Find(m =>m.ReceiverId == userId ||
            m.SenderId == userId).ToListAsync();
        return result;    
    }

    public async Task<List<User>> GetUsersWhoMessagedAdminAsync()
    {
        var pipeline = _chatMessages.Find(s => s.Receiver == "Admin").ToListAsync();

        var users = new List<User>();

        foreach (var result in pipeline.Result)
        {
            var user = await _userService.GetUserByUserNameAsync(result.Sender);
            if (user != null && !users.Any(u => u.Id == user.Id))
            {
                users.Add(user);
            }
        }

        return users;
    }

    public async Task<ChatMessage?> GetMessageByIdAsync(string messageId)
    {
        var filter = Builders<ChatMessage>.Filter.Eq(m => m.Id, messageId);
        return await _chatMessages.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateMessageAsync(ChatMessage message)
    {
        var filter = Builders<ChatMessage>.Filter.Eq(m => m.Id, message.Id);
        await _chatMessages.ReplaceOneAsync(filter, message);
    }

    public async Task<List<ChatMessage>> GetAllMessages()
    {
        return await _chatMessages.Find(m => m.Receiver == "Admin").ToListAsync();
    }

    public async Task<int> CountUnreadMessagesById(string userId, CancellationToken ct = default)
    {
        var filter = Builders<ChatMessage>.Filter.Where(m => m.ReceiverId == userId && !m.Read);
        var unreadMessagesCount = await _chatMessages.CountDocumentsAsync(filter, cancellationToken: ct);
        return (int)unreadMessagesCount;
    }
}
