using CarRental.Support.Chat.Model;
using System.Security.Claims;

namespace CarRental.Support.Chat.Services.Interface;

#nullable enable
public interface IChatService
{
    Task SendToSupportMessage(ClaimsPrincipal user, string message);

    Task<IEnumerable<string>> GetChatMessagesAsync(string userId);

    Task<IEnumerable<ChatMessage>> GetAllMessagesAsync();
}
