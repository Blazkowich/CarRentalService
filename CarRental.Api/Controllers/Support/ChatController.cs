using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Support
{
    [Route("chat")]
    [ApiController]
    [Authorize]
    public class ChatController(IChatMapped chatMapped) : ControllerBase
    {
        private readonly IChatMapped _chatMapped = chatMapped;

        [HttpPost("sendtoadmin")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest chat)
        {
            await _chatMapped.SendToSupportMessage(HttpContext.User, chat.Message);
            return Ok();
        }

        [HttpPost("sendtouser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendToUserMessage([FromBody] ChatRequest chat)
        {
            await _chatMapped.SendMessageToUser(HttpContext.User, chat.UserId, chat.Message);
            return Ok();
        }

        [HttpGet("messages/{userId}")]
        public async Task<IActionResult> GetChatMessages(string userId)
        {
            var messages = await _chatMapped.GetChatMessagesAsync(userId);
            return Ok(messages);
        }

        [HttpGet("messages/foradmin/{userId}")]
        public async Task<IActionResult> GetChatMessagesForAdmin(string userId)
        {
            var messages = await _chatMapped.GetChatMessagesForAdminAsync(userId);
            return Ok(messages);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetAllChatMessages()
        {
            var allMessages = await _chatMapped.GetAllMessagesAsync();
            return Ok(allMessages);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsersByMessages")]
        public async Task<IActionResult> GetUsersWhoMessagedAdmin()
        {
            var users = await _chatMapped.GetUsersWhoMessagedAdminAsync();
            return Ok(users);
        }

        [HttpGet("unread/count/{userId}")]
        public async Task<IActionResult> CountUnreadMessages(string userId)
        {
            var count = await _chatMapped.GetUnreadMessageCountAsync(userId);
            return Ok(count);
        }

        [HttpPatch("messages/{messageId}/read")]
        public async Task<IActionResult> MarkMessageAsRead(string messageId)
        {
            try
            {
                await _chatMapped.MarkMessageAsReadAsync(HttpContext.User, messageId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
