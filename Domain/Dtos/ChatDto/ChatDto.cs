using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ChatDto;

public class ChatDto
{
    [Required]
    public int ChatId { get; set; }

    [Required] public string ReceiveUserId { get; set; } = null!;
}