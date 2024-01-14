using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Post;
public class StoryUser
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int StoryId { get; set; }
    
    public User.User User { get; set; }
    public Story Story { get; set; }
}