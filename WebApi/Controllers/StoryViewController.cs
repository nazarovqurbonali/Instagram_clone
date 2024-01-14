/*using Domain.Dtos.StoryViewDtos;
using Domain.Responses;
using Infrastructure.Services.StoryViewServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class StoryViewController : BaseController
{
    private readonly IStoryViewService _storyViewService;

    public StoryViewController(IStoryViewService storyViewService)
    {
        _storyViewService = storyViewService;
    }

    [HttpPost("add-story-view")]
    public Task<Response<GetStoryViewDto>> AddStoryView(AddStoryViewDto model)
    {
        var token = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        return _storyViewService.AddStoryView(model, token);
    } 
}*/