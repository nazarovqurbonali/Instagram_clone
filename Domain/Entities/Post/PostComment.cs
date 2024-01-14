using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class PostComment
{
    public int PostCommentId { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public string UserId { get; set; }
    public User.User User { get; set; }
    public string Comment { get; set; }
    public DateTime DateCommented { get; set; }
    public PostCommentLike PostCommentLike { get; set; }
}