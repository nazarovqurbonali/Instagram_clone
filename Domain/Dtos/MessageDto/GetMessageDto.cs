namespace Domain.Dtos.MessageDto;

public class GetMessageDto : MessageDto
{
    public string UserId { get; set; } = null!;
    public int MessageId { get; set; }
    public DateTime SendMassageDate { get; init; }
}