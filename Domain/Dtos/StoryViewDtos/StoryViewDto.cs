namespace Domain.Dtos.StoryViewDtos;

public class StoryViewDto
{
    public int Id { get; set; }
    public string ViewUserId { get; set; } = null!;
    public int StoryId { get; set; }
}