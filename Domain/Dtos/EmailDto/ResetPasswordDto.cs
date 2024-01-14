using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmailDto;

public class ResetPasswordDto
{
    [Required]
    public string Token { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password"), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}