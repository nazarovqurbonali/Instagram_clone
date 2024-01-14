using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities;

public class Location
{
    public int LocationId { get; set; }
    [MaxLength(45)]
    public string City { get; set; }
    [MaxLength(45)]
    public string State { get; set; }
    [MaxLength(45)]
    public string ZipCode { get; set; }
    [MaxLength(45)]
    public string Country { get; set; }
    public List<UserProfile> UserProfiles { get; set; }
}