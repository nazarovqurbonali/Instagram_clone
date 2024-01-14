using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostCommentDto;

public class AddPostCommentDto : PostCommentDto
{
    [Required]
    public int PostId { get; set; }
}