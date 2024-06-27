using CarRental.Auth.BLL.Models;
using CarRental.Support.Chat.Model;

#nullable enable

namespace CarRental.Support.Chat.Services.Interface;

public interface IChatMessageService
{
    Task SaveMessage(ChatMessage message);
    Task<List<ChatMessage>> GetMessages(string userId);

    Task<List<ChatMessage>> GetAllMessages();

    Task<List<User>> GetUsersWhoMessagedAdminAsync();

    Task<ChatMessage?> GetMessageByIdAsync(string messageId);

    Task UpdateMessageAsync(ChatMessage message);

    Task<int> CountUnreadMessagesById(string userId, CancellationToken ct = default);
}