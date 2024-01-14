namespace Domain.Entities.Post;

public class StoryView
{
    public int Id { get; set; }
    public string ViewUserId { get; set; }
    public int StoryId { get; set; }

    public Story Story { get; set; }
    
}