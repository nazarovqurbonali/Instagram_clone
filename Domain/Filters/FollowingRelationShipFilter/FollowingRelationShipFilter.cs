using System.ComponentModel.DataAnnotations;

namespace Domain.Filters.FollowingRelationShipFilter;

public class FollowingRelationShipFilter
{
    [Required]
    public string UserId { get; set; } = null!;
}