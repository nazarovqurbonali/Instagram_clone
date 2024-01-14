using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.UserSearchHistoryDto;

public class AddUserSearchHistoryDto : UserSearchHistoryDto
{
    [Required]
    public string UserSearchId { get; set; } = null!;
}