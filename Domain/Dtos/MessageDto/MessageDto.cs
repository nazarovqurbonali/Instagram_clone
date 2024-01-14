using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.MessageDto;

public class MessageDto
{
    [Required]
    public int ChatId { get; set; }
    [Required]
    public string MessageText { get; set; } = null!;
}