using System.ComponentModel.DataAnnotations;
using Domain.Dtos;
using Domain.Dtos.ViewerDtos;
using Domain.Entities.Post;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Story
{
    [Key]
    public int Id { get; set; }
    public string? FileName { get; set; }
    public int? PostId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; }

    public List<StoryView> StoryViews { get; set; }
    public Post.Post Post { get; set; }
    public List<StoryLike> StoryLikes { get; set; }
    public StoryStat StoryStat{ get; set; }
    public List<StoryUser> StoryUsers { get; set; }
}