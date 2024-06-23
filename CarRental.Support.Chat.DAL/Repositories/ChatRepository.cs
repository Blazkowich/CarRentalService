using CarRental.Support.Chat.DAL.Context;
using CarRental.Support.Chat.DAL.Context.Entities;
using CarRental.Support.Chat.DAL.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Support.Chat.DAL.Repositories;

public class ChatRepository(IHubContext<ChatHub> hubContext, CarRentalChatDbContext dbContext)
{
    private readonly IHubContext<ChatHub> _hubContext = hubContext;
    private readonly CarRentalChatDbContext _dbContext = dbContext;

    public async Task SendMessage(string userName, string messageContent)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == userName);
        if (user == null)
        {
            user = new UserEntity { Name = userName };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        var message = new MessageEntity
        {
            Content = messageContent,
            Timestamp = DateTime.Now,
            UserId = user.Id
        };

        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }
}
