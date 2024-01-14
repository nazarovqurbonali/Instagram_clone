using Domain.Dtos.UserDto;

namespace Domain.Dtos.FollowingRelationshipDto;

public class SubscriptionsDto
{
    public int Id { get; set; }
    public GetUserShortInfoDto UserShortInfo { get; set; } = null!;
}