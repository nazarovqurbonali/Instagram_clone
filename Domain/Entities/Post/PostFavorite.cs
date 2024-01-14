using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Post;

public class PostFavorite
{
    [Key]
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int FavoriteCount { get; set; }
    public List<PostFavoriteUser> PostFavoriteUsers { get; set; }
}