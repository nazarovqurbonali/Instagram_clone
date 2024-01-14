using Domain.Responses;

namespace Infrastructure.Services.StatisticFollowAndPostService;

public interface IStatisticFollowAndPostService
{
    Task<Response<int>> GetUserPost(string userId);
    Task<Response<int>> GetFollowing(string userId);
    Task<Response<int>> GetFollowers(string userId);
}