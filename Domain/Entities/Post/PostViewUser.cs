using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class PostViewUser
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public User.User User { get; set; }
    public int PostViewId { get; set; }
    public PostView PostView { get; set; }
}