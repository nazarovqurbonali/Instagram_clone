using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Post;

public class PostLike
{
    [Key] public int PostId { get; set; }
    public int LikeCount { get; set; }
    public Post Post { get; set; }
    public List<PostUserLike> PostUserLikes { get; set; }
}