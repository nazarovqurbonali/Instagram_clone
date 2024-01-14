using System.Net;
using Domain.Dtos.MessageDto;
using Domain.Responses;
using Infrastructure.Services.ChatService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ChatController(IChatService service) : BaseController
{
    [HttpGet("get-chats")]
    public async Task<IActionResult> GetChats()
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
            var result = await service.GetChats(userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<List<MessageDto>>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("get-chat-by-id")]
    public async Task<IActionResult> GetChatById(int chatId)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetChatById(chatId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<List<MessageDto>>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("create-chat")]
    public async Task<IActionResult> CreateChat(string receiverUserId)
    {
        var sendUserId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
        var response = await service.CreateChat(sendUserId, receiverUserId);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("send-message")]
    public async Task<IActionResult> SendMessage([FromBody]MessageDto message)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
                var result = await service.SendMessage(message, userId);
            return StatusCode(result.StatusCode, result);
        }
        
        var response = new Response<List<MessageDto>>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-message")]
    public async Task<IActionResult> DeleteMessage(int massageId)
    {
        var result = await service.DeleteMessage(massageId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("delete-chat")]
    public async Task<IActionResult> DeleteChat(int chatId)
    {
        var result = await service.DeleteChat(chatId);
        return StatusCode(result.StatusCode, result);
    }
}