using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.EmailDto;

public class ChangePasswordDto
{
    [Required]
    public string OldPassword { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required]
    [Compare("Password"), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

}