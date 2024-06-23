using CarRental.Support.Chat.DAL.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Support.Chat.DAL.Services;

public class ChatHub() : Hub
{
    public async Task SendMessage(string userName, string messageContent)
    {
        var chatService = Context.GetHttpContext().RequestServices.GetService<ChatRepository>();
        await chatService.SendMessage(userName, messageContent);
    }
}
