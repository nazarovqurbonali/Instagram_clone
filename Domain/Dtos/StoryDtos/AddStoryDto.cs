using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.StoryDtos;

public class AddStoryDto
{
    public int? PostId { get; set; }
    public IFormFile? Image { get; set; }
}