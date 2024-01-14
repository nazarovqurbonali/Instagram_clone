using System.Net;
using AutoMapper;
using Domain.Dtos.StoryViewDtos;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StoryViewServices;

public class StoryViewService : IStoryViewService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StoryViewService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GetStoryViewDto>> AddStoryView(AddStoryViewDto model, string userId)
    {
        try
        {
            var story = _context.Stories.FirstOrDefault(e => e.Id == model.StoryId);
            if (story != null)
            {
                var storyView = new StoryView()
                {
                    ViewUserId = userId,
                    StoryId = model.StoryId,
                };
                var existView =
                    await _context.StoryUsers.FirstOrDefaultAsync(e =>
                        e.StoryId == model.StoryId && e.UserId == userId);
                var stat = _context.StoryStats.FirstOrDefault(e => e.StoryId == story.Id);
                if (existView == null)
                {
                    stat.ViewCount++;
                    var view = new StoryUser()
                    {
                      StoryId = model.StoryId,
                      UserId = userId
                    };
                    await _context.StoryViews.AddAsync(storyView);
                    _context.StoryStats.Update(stat);
                    await _context.StoryUsers.AddAsync(view);
                    await _context.SaveChangesAsync();
                    var mapped = _mapper.Map<GetStoryViewDto>(storyView);
                    return new Response<GetStoryViewDto>(mapped);
                }
                else
                {
                    var mapped = _mapper.Map<GetStoryViewDto>(storyView);
                    return new Response<GetStoryViewDto>(mapped);
                }
            }
            else
            {
                return new Response<GetStoryViewDto>(HttpStatusCode.BadRequest, "Story not found");
            }
        }
        catch (Exception e)
        {
            return new Response<GetStoryViewDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}