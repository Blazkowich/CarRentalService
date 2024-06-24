using CarRental.Support.Chat.Context;
using CarRental.Support.Chat.Services;
using CarRental.Support.Chat.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Support.Chat.DependencyInjection;

public static class CarRentalSupportChatServiceExtension
{
    public static IServiceCollection AddSupportChatBLLServices(this IServiceCollection services)
    {
        services.AddSingleton(sp => new ChatContext("mongodb://localhost:27017", "CarRental"));
        services.AddScoped<IChatMessageService, ChatMessageService>();
        services.AddScoped<IChatService, ChatService>();
        return services;
    }
}
