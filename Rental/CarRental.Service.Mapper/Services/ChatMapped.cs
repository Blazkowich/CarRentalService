using CarRental.Auth.BLL.Models;
using CarRental.Service.Mapper.Services.Interfaces;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using System.Security.Claims;

namespace CarRental.Service.Mapper.Services
{
    internal class ChatMapped : IChatMapped
    {
        private readonly IChatService _chatService;

        public ChatMapped(IChatService chatService)
        {
            _chatService = chatService;
        }

        public Task SendToSupportMessage(ClaimsPrincipal user, string message)
        {
            return _chatService.SendToSupportMessage(user, message);
        }

        public Task SendMessageToUser(ClaimsPrincipal user, string userId, string message)
        {
            return _chatService.SendMessageToUser(user, userId, message);
        }

        public Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(string userId)
        {
            return _chatService.GetChatMessagesAsync(userId);
        }

        public Task<IEnumerable<ChatMessage>> GetChatMessagesForAdminAsync(string userId)
        {
            return _chatService.GetChatMessagesForAdminAsync(userId);
        }

        public Task<IEnumerable<ChatMessage>> GetAllMessagesAsync()
        {
            return _chatService.GetAllMessagesAsync();
        }

        public Task<List<User>> GetUsersWhoMessagedAdminAsync()
        {
            return _chatService.GetUsersWhoMessagedAdminAsync();
        }

        public Task<int> GetUnreadMessageCountAsync(string userId)
        {
            return _chatService.GetUnreadMessageCountAsync(userId);
        }

        public Task MarkMessageAsReadAsync(ClaimsPrincipal user, string messageId)
        {
            return _chatService.MarkMessageAsReadAsync(user, messageId);
        }
    }
}
