using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ExternalAccountDto;

public class ExternalAccountDto
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public string FacebookEmail { get; set; } = null!;
    [Required]
    public string TwitterUsername { get; set; } = null!;
}