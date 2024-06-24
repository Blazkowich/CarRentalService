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
    public async Task<IActionResult> SendMessage([FromBody] string message)
    {
        await _chatService.SendToSupportMessage(HttpContext.User, message);
        return Ok();
    }

    [HttpGet("messages/{userId}")]
    public async Task<IActionResult> GetChatMessages(string userId)
    {
        var messages = await _chatService.GetChatMessagesAsync(userId);
        return Ok(messages);
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetAllChatMessages()
    {
        var allMessages = await _chatService.GetAllMessagesAsync();
        return Ok(allMessages);
    }

    // იუზერის შეზღუდვა სხვა მესიჯების ნახვაზე როლების დახმარებით
    // დამატებითი მეთოდების დამატება
}
