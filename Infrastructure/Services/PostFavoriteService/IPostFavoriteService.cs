using Domain.Dtos.PostDto;
using Domain.Dtos.PostFavoriteDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PostFavoriteService;

public interface IPostFavoriteService
{
    Task<PagedResponse<List<GetPostDto>>> GetPostFavorites(PaginationFilter filter, string userId);
    Task<Response<GetPostFavoriteDto>> GetPostFavoriteById(int id);
    Task<Response<bool>> AddPostFavorite(AddPostFavoriteDto addPostFavorite);
    Task<Response<bool>> DeletePostFavorite(int id);
}