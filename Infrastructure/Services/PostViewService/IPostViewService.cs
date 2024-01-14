using Domain.Responses;

namespace Infrastructure.Services.PostViewService;

public interface IPostViewService
{
    Task<Response<bool>> AddViewToPost(string userId, int postId);
    Task<Response<bool>> DeleteViewToPost(string userId, int postId);
}