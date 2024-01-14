using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Post;

public class PostCommentLike
{
    [Key]
    public int PostCommentId { get; set; }
    public int LikeCount { get; set; }
    public PostComment PostComment { get; set; }
}
