using Domain.Dtos.UserDto;

namespace Domain.Dtos.FollowingRelationshipDto;

public class SubscribersDto
{
    public int Id { get; set; }
    public GetUserShortInfoDto UserShortInfo { get; set; } = null!;
}