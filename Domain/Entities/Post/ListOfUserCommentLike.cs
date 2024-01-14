using Domain.Entities.Post;

namespace Domain.Entities.User;

public class ListOfUserCommentLike
{
    public int Id { get; set; }
    public int PostCommentLikeId { get; set; }
    public PostCommentLike PostCommentLike { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}