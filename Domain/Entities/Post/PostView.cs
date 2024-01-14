using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class PostView
{
    [Key]
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int ViewCount { get; set; }
    public List<PostViewUser> PostViewUsers { get; set; }
}