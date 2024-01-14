namespace Domain.Entities;

public class StoryStat
{
    public int Id { get; set; }
    public int ViewCount { get; set; } = 0;
    public int ViewLike { get; set; } = 0;
    public int StoryId { get; set; }
    public Story Story { get; set; }
}