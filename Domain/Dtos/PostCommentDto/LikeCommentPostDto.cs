using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostCommentDto;

public class LikeCommentPostDto
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public int PostId { get; set; }
}
