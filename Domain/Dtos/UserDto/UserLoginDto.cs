using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.UserDto;

public class UserLoginDto
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}