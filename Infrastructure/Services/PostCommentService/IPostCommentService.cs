using Domain.Dtos.PostCommentDto;
using Domain.Filters.PostCommentFilter;
using Domain.Responses;

namespace Infrastructure.Services.PostCommentService;

public interface IPostCommentService
{
    Task<PagedResponse<List<GetPostCommentDto>>> GetPostComments(PostCommentFilter filter);
    Task<Response<GetPostCommentDto>> GetPostCommentById(int id);
    Task<Response<GetPostCommentDto>> AddPostComment(AddPostCommentDto addPostComment);
    Task<Response<bool>> DeletePostComment(int id);
} 