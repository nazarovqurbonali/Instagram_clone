using System.Net;
using Domain.Dtos.ChatDto;
using Domain.Dtos.MessageDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ChatService;

public class ChatService(DataContext context) : IChatService
{
    public async Task<Response<List<GetChatDto>>> GetChats(string? userId)
    {
        try
        {
            var chats = await context.Chats.Where(u => u.SendUserId == userId || u.ReceiveUserId == userId)
                .Select(c => new GetChatDto()
                {
                    ChatId = c.ChatId,
                    SendUserId = c.SendUserId,
                    ReceiveUserId = c.ReceiveUserId
                }).ToListAsync();
            return new Response<List<GetChatDto>>(chats);
        }
        catch (Exception e)
        {
            return new Response<List<GetChatDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetMessageDto>>> GetChatById(int chatId)
    {
        try
        {
            var chat = await context.Chats.FindAsync(chatId);
            if (chat == null) return new Response<List<GetMessageDto>>(HttpStatusCode.BadRequest, "Chat not found");
            var response = await (from c in context.Chats
                join m in context.Messages on c.ChatId equals m.ChatId
                where c.ChatId == chatId
                select new GetMessageDto()
                {
                    MessageId = m.MessageId,
                    ChatId = m.ChatId,
                    UserId = m.UserId,
                    MessageText = m.MessageText,
                    SendMassageDate = m.SendMassageDate
                }).OrderByDescending(x => x.SendMassageDate).ToListAsync();
            return new Response<List<GetMessageDto>>(response);
        }
        catch (Exception e)
        {
            return new Response<List<GetMessageDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> CreateChat(string sendUserId, string receiveUserId)
    {
        try
        {
            if (sendUserId == receiveUserId)
                return new Response<int>(HttpStatusCode.BadRequest, "You can't create a chat with yourself");
            var result = await context.Chats.FirstOrDefaultAsync(u =>
                (u.SendUserId == sendUserId && u.ReceiveUserId == receiveUserId) ||
                (u.ReceiveUserId == sendUserId) && u.SendUserId == receiveUserId);
            if (result == null)
            {
                var newChat = new Chat()
                {
                    SendUserId = sendUserId,
                    ReceiveUserId = receiveUserId
                };
                await context.Chats.AddAsync(newChat);
                await context.SaveChangesAsync();
                return new Response<int>(newChat.ChatId);
            }

            return new Response<int>(result.ChatId);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> SendMessage(MessageDto message, string userId)
    {
        try
        {
            var chat = await context.Chats.FindAsync(message.ChatId);
            if (chat == null) return new Response<int>(HttpStatusCode.BadRequest, "Chat not found!");
            var newMessage = new Message()
            {
                ChatId = message.ChatId,
                UserId = userId,
                MessageText = message.MessageText,
                SendMassageDate = DateTime.UtcNow
            };
            await context.Messages.AddAsync(newMessage);
            await context.SaveChangesAsync();
            return new Response<int>(newMessage.MessageId);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMessage(int massageId)
    {
        try
        {
            var message = await context.Messages.FindAsync(massageId);
            if (message == null) return new Response<bool>(false);
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteChat(int chatId)
    {
        var chat = await context.Chats.FindAsync(chatId);
        if (chat == null) return new Response<bool>(false);
        context.Chats.Remove(chat);
        await context.SaveChangesAsync();
        return new Response<bool>(true);
    }
}