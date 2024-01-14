using Domain.Entities;
using Domain.Entities.Post;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.StoryDtos;

public class StoryDto
{
   
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public int? PostId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = null!;
}