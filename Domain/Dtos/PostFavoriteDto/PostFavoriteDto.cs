using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostFavoriteDto;

public class PostFavoriteDto
{
    [Required]
    public int PostId { get; set; }
}