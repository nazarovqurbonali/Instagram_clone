using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.PostDto;

public class PostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    [Required]
    public List<IFormFile> Images { get; set; } = null!;
}