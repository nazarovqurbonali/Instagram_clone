using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.StoryViewDtos;

public class AddStoryViewDto
{
    [Required]
    public int StoryId { get; set; }
}