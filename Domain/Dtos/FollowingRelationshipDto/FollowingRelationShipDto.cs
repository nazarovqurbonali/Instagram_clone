using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.FollowingRelationshipDto;

public class FollowingRelationShipDto
{
    [Required]
    public string FollowingId { get; set; } = null!;
}