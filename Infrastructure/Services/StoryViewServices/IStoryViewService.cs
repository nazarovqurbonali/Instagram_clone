using Domain.Dtos.StoryViewDtos;
using Domain.Responses;

namespace Infrastructure.Services.StoryViewServices;

public interface IStoryViewService
{
    Task<Response<GetStoryViewDto>> AddStoryView(AddStoryViewDto model,string token);
}