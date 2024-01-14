using Domain.Dtos.PostCommentDto;
using Domain.Dtos.UserDto;

namespace Domain.Dtos.PostDto;

public class GetPostDto : PostDto
{
    public int PostId { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime DatePublished { get; set; }
    public new List<string> Images { get; set; } = null!;
    public bool PostLike { get; set; }
    public int PostLikeCount { get; set; }
    public List<GetUserShortInfoDto>? UserLikes { get; set; }
    public int CommentCount { get; set; }
    public List<GetPostCommentDto>? Comments { get; set; }
    public int PostView { get; set; }
    public List<GetUserShortInfoDto>? UserViews { get; set; }
    public bool PostFavorite { get; set; }
    public List<GetUserShortInfoDto>? UserFavorite { get; set; }
}