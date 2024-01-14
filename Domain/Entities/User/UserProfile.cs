using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Post;
using Domain.Enums;

namespace Domain.Entities.User;

public class UserProfile
{
    [Key, ForeignKey("User")] public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? LocationId { get; set; } 
    public Location Location { get; set; } = null!;
    public string? Image { get; set; }
    public Gender? Gender { get; set; }
    public DateTime Dob { get; set; } = DateTime.UtcNow;
    public string? Occupation { get; set; }
    public string? About { get; set; }
    public DateTime DateUpdated { get; set; }
}