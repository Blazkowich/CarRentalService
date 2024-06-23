using CarRental.Support.Chat.Model;

namespace CarRental.Support.Chat.Services.Interface;

public interface IChatMessageService
{
    Task SaveMessage(ChatMessage message);
    Task<List<ChatMessage>> GetMessages(Guid userId);

    Task<List<ChatMessage>> GetAllMessages();
}