using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading.Tasks;

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

            var userConnection = new User
            {
                Id = customer.Id,
                Name = customer.Name,
            };

            var chatMessage = new ChatMessage
            {
                Sender = userConnection.Name,
                SenderId = Guid.Parse(userConnection.Id),
                Receiver = "Admin",
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await _chatMessageService.SaveMessage(chatMessage);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", new
            {
                from = userConnection.Name,
                text = message,
                isIncoming = true
            });

            string chatId = $"{userConnection.Id}_{userConnection.Name}";
            _userChats.AddOrUpdate(chatId, userConnection.Name, (key, oldValue) => userConnection.Name);
        }

        public async Task<IEnumerable<string>> GetUserChats()
        {
            return await Task.FromResult(_userChats.Values);
        }

        public async Task<IEnumerable<string>> GetChatMessagesAsync(string userId)
        {
            var messages = await _chatMessageService.GetMessages(Guid.Parse(userId));

            var messageStrings = messages.Select(m => $"{m.Sender} - {m.Message} - {m.Timestamp}");

            return messageStrings;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessagesAsync()
        {
            var messages = await _chatMessageService.GetAllMessages();

            return messages;
        }
    }
}
