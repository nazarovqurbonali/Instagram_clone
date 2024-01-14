using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.LoginDto;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}