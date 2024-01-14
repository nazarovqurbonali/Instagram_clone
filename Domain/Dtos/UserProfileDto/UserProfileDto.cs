using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDto;

public class UserProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? LocationId { get; set; }
    public IFormFile? Image { get; set; }
    public DateTime Dob { get; set; }
    public string? Occupation { get; set; }
    public string? About { get; set; }
}