using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.RegisterDto;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password")][DataType(DataType.Password)]
    [Required]
    public string ConfirmPassword { get; set; } = null!;
}