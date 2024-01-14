using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Post;

[Index("UserId", "PostLikeId", IsUnique = true)]
public class PostUserLike
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User.User User { get; set; } = null!;
    public int PostLikeId { get; set; }
    public PostLike PostLike { get; set; } = null!;
}