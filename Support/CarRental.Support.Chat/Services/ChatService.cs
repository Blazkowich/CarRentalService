using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace CarRental.Support.Chat.Services
{
    public class ChatService(IChatMessageService chatMessageService, IUserService userService, IHubContext<ChatService> hubContext) : Hub, IChatService
    {
        private readonly IChatMessageService _chatMessageService = chatMessageService;
        private readonly IUserService _userService = userService;
        private readonly IHubContext<ChatService> _hubContext = hubContext;
        private readonly ConcurrentDictionary<string, string> _userChats = new();

        public async Task SendToSupportMessage(ClaimsPrincipal user, string message)
        {
            var customerName = user.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new InvalidOperationException("Customer Id not found in claims.");

            var customer = await _userService.GetUserByUserNameAsync(customerName)
                ?? throw new InvalidOperationException($"User with name '{customerName}' not found.");

            var reciverId = await _userService.GetUserByUserNameAsync("Admin");

            var userConnection = new User
            {
                Id = customer.Id,
                Name = customer.Name,
            };

            var chatMessage = new ChatMessage
            {
                Sender = userConnection.Name,
                SenderId = userConnection.Id,
                Receiver = "Admin",
                ReceiverId = reciverId.Id,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await _chatMessageService.SaveMessage(chatMessage);

            await _hubContext.Clients.Group("Admin").SendAsync("ReceiveMessage", userConnection.Name, message);
        }

        public async Task SendMessageToUser(ClaimsPrincipal currentUser, string userId, string message)
        {
            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId))
                ?? throw new InvalidOperationException($"User with ID '{userId}' not found.");

            var adminName = currentUser.FindFirst(ClaimTypes.Name)?.Value
               ?? throw new InvalidOperationException("Customer Id not found in claims.");

            var admin = await _userService.GetUserByUserNameAsync(adminName)
                ?? throw new InvalidOperationException($"User with name '{adminName}' not found.");

            var userConnection = new User
            {
                Id = user.Id,
                Name = user.Name,
            };

            var chatMessage = new ChatMessage
            {
                Sender = "Admin",
                SenderId = admin.Id,
                Receiver = userConnection.Name,
                ReceiverId = userId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await _chatMessageService.SaveMessage(chatMessage);

            await _hubContext.Clients.User(userConnection.Id.ToString()).SendAsync("ReceiveMessage", "Admin", message);
        }


        public async Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(string userId)
        {
            var messages = await _chatMessageService.GetMessages(userId);

            return messages;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesForAdminAsync(string userId)
        {
            var messages = await _chatMessageService.GetMessages(userId);

            return messages;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessagesAsync()
        {
            var messages = await _chatMessageService.GetAllMessages();

            return messages;
        }

        public async Task MarkMessageAsReadAsync(ClaimsPrincipal currentUser, string messageId)
        {
            var customerName = currentUser.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new InvalidOperationException("Customer Id not found in claims.");

            var customer = await _userService.GetUserByUserNameAsync(customerName)
                ?? throw new InvalidOperationException($"User with name '{customerName}' not found.");

            var message = await _chatMessageService.GetMessageByIdAsync(messageId) ??
                throw new InvalidOperationException($"Message with ID '{messageId}' not found.");
            if (customer.Id != message.SenderId)
            {
                message.Read = true;
                await _chatMessageService.UpdateMessageAsync(message);
            }
        }

        public async Task<List<User>> GetUsersWhoMessagedAdminAsync()
        {
            var users = await _chatMessageService.GetUsersWhoMessagedAdminAsync();
            return users;
        }

        public async Task<int> GetUnreadMessageCountAsync(string userId)
        {
            return await _chatMessageService.CountUnreadMessagesById(userId);
        }
    }
}
