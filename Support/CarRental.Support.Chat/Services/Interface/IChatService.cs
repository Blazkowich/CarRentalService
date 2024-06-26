using CarRental.Auth.BLL.Models;
using CarRental.Support.Chat.Model;
using System.Security.Claims;

namespace CarRental.Support.Chat.Services.Interface;

#nullable enable
public interface IChatService
{
    Task SendToSupportMessage(ClaimsPrincipal user, string message);

    Task SendMessageToUser(ClaimsPrincipal currentUser, string userId, string message);

    Task<IEnumerable<string>> GetChatMessagesAsync(string userId);

    Task<IEnumerable<string>> GetChatMessagesForAdminAsync(string userId);

    Task<IEnumerable<ChatMessage>> GetAllMessagesAsync();

    Task<List<User>> GetUsersWhoMessagedAdminAsync();
}
