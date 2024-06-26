using CarRental.Api.ApiModels.Request;
using CarRental.Support.Chat.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Support;

[Route("chat")]
[ApiController]
[Authorize]
public class ChatController(
    IChatService chatService) : ControllerBase
{
    private readonly IChatService _chatService = chatService;

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest chat)
    {
        await _chatService.SendToSupportMessage(HttpContext.User, chat.Message);
        return Ok();
    }

    [HttpPost("sendtouser")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendToUserMessage([FromBody] ChatRequest chat)
    {
        await _chatService.SendMessageToUser(HttpContext.User, chat.UserId, chat.Message);
        return Ok();
    }

    [HttpGet("messages/{userId}")]
    public async Task<IActionResult> GetChatMessages(string userId)
    {
        var messages = await _chatService.GetChatMessagesAsync(userId);
        return Ok(messages);
    }

    [HttpGet("messages/foradmin/{userId}")]
    public async Task<IActionResult> GetChatMessagesForAdmin(string userId)
    {
        var messages = await _chatService.GetChatMessagesForAdminAsync(userId);
        return Ok(messages);
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetAllChatMessages()
    {
        var allMessages = await _chatService.GetAllMessagesAsync();
        return Ok(allMessages);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("getUsersByMessages")]
    public async Task<IActionResult> GetUsersWhoMessagedAdmin()
    {
        var users = await _chatService.GetUsersWhoMessagedAdminAsync();
        return Ok(users);
    }

    // იუზერის შეზღუდვა სხვა მესიჯების ნახვაზე როლების დახმარებით
    // დამატებითი მეთოდების დამატება
}
