using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class PostFavoriteUser
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public User.User User { get; set; }
    public int PostFavoriteId { get; set; }
    public PostFavorite PostFavorite { get; set; }
}