using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostCommentDto;

public class PostCommentDto
{
    [Required]
    public string Comment { get; set; } = null!;
}