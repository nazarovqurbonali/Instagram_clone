using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmailDto;

public class ForgotPasswordDto
{
    [Required]
    public string Email { get; set; } = null!;
}