using CarRental.Auth.BLL.Models;
using CarRental.Support.Chat.Model;
using CarRental.Support.Chat.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.Support.Chat.Services
{
    public class ChatHub(IChatService chatService, IChatMessageService chatMessageService) : Hub
    {
        private readonly IChatService _chatService = chatService;
        private readonly IChatMessageService _chatMessageService = chatMessageService;

        public async Task SendMessageToAdmin(string user, string message)
        {
            var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var adminId = "ac100f97-6db1-42ba-b3ad-a0881b167e50";

            var chatMessage = new ChatMessage
            {
                Sender = user,
                SenderId = senderId,
                Receiver = "Admin",
                ReceiverId = adminId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await SaveAndSendToGroup("Admin", chatMessage);
        }

        public async Task SendMessageToUser(string admin, string userId, string message)
        {
            var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var chatMessage = new ChatMessage
            {
                Sender = admin,
                SenderId = senderId,
                ReceiverId = userId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await SaveAndSendToUser(userId, chatMessage);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Admin"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            }
            await base.OnConnectedAsync();
        }

        public async Task GetUsersByMessages()
        {
            var users = await _chatService.GetUsersWhoMessagedAdminAsync();
            await Clients.Caller.SendAsync("ReceiveUsersByMessages", users);
        }

        private async Task SaveAndSendToGroup(string groupName, ChatMessage chatMessage)
        {
            await _chatMessageService.SaveMessage(chatMessage);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", chatMessage.Sender, chatMessage.Message);
        }

        private async Task SaveAndSendToUser(string userId, ChatMessage chatMessage)
        {
            await _chatMessageService.SaveMessage(chatMessage);
            await Clients.User(userId).SendAsync("ReceiveMessage", chatMessage.Sender, chatMessage.Message);
        }
    }
}
