using CarRental.Auth.BLL.Models;
using CarRental.Support.Chat.Model;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services.Interfaces
{
    public interface IChatMapped
    {
        Task SendToSupportMessage(ClaimsPrincipal user, string message);
        Task SendMessageToUser(ClaimsPrincipal user, string userId, string message);
        Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(string userId);
        Task<IEnumerable<ChatMessage>> GetChatMessagesForAdminAsync(string userId);
        Task<IEnumerable<ChatMessage>> GetAllMessagesAsync();
        Task<List<User>> GetUsersWhoMessagedAdminAsync();
        Task<int> GetUnreadMessageCountAsync(string userId);
        Task MarkMessageAsReadAsync(ClaimsPrincipal user, string messageId);
    }
}
