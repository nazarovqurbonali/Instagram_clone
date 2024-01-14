using Domain.Dtos.FollowingRelationshipDto;
using Domain.Filters.FollowingRelationShipFilter;
using Domain.Responses;

namespace Infrastructure.Services.FollowingRelationShipService;

public interface IFollowingRelationShipService
{
    Task<Response<GetFollowingRelationShipDto>> GetFollowingRelationShip(FollowingRelationShipFilter filter);
    Task<Response<List<SubscribersDto>>> GetSubscribers(FollowingRelationShipFilter filter);
    Task<Response<List<SubscriptionsDto>>> GetSubscriptions(FollowingRelationShipFilter filter);
    Task<Response<bool>> AddFollowingRelationShip(string followingUserId, string userId);
    Task<Response<bool>> DeleteFollowingRelationShip(int id);
}