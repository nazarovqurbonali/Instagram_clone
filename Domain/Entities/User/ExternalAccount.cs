using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.User;

public class ExternalAccount
{
    [Key]
    public string UserId { get; set; }
    public User User { get; set; }
    [MaxLength(45)]
    public string? FacebookEmail { get; set; }
    [MaxLength(45)]
    public string? TwitterUsername { get; set; }
}