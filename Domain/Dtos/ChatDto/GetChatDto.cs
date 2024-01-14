namespace Domain.Dtos.ChatDto;

public class GetChatDto : ChatDto
{
    public string SendUserId { get; set; } = null!;
}