using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.SearchHistoryDto;

public class SearchHistoryDto
{
    [Required]
    public string Text { get; set; } = null!;
}