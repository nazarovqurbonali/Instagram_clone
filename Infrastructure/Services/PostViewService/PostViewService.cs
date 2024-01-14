using System.Net;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PostViewService;

public class PostViewService : IPostViewService
{
    private readonly DataContext _context;

    public PostViewService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Response<bool>> AddViewToPost(string userId, int postId)
    {
        try
        {
            var userView = await _context.PostViewUsers.FindAsync(userId);
            if (userView != null) return new Response<bool>(HttpStatusCode.BadRequest, "You have already viewed this post");
            var postStat = await _context.PostViews.FirstOrDefaultAsync(p => p.PostId == postId);
            if (postStat == null) return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");
            postStat.ViewCount++;
            var user0 = new PostViewUser()
            {
                UserId = userId,
                PostViewId = postId
            };
            await _context.PostViewUsers.AddAsync(user0);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteViewToPost(string userId, int postId)
    {
        try
        {
            var userView = await _context.PostViewUsers.FindAsync(userId);
            if (userView != null) return new Response<bool>(HttpStatusCode.BadRequest, "You have already viewed this post");
            var postView = await _context.PostViews.FirstOrDefaultAsync(p => p.PostId == postId);
            if (postView == null) return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");
            postView.ViewCount--;
            _context.PostViewUsers.Remove(userView);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}