using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class Post
{
    public int PostId { get; set; }
    public string UserId { get; set; } = null!;
    public User.User User { get; set; } = null!;
    [MaxLength(45)]
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime DatePublished { get; set; }
    public List<Story>? Stories { get; set; }
    public List<PostComment> PostComments { get; set; } = null!;
    public PostFavorite PostFavorite { get; set; } = null!;
    public PostView PostView { get; set; } = null!;
    public PostLike PostLike { get; set; } = null!;
    public List<PostCategory> PostCategories { get; set; } = null!;
    public List<Image> Images { get; set; } = null!;
}