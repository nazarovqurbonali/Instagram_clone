using System.Net;
using AutoMapper;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StatisticFollowAndPostService;

public class StatisticFollowAndPostService:IStatisticFollowAndPostService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    public StatisticFollowAndPostService(DataContext context, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }
     

    public async Task<Response<int>> GetUserPost(string userId)
    {
        try
        {
            var existing = _context.Users.FirstOrDefault(x => x.Id == userId)!;

            if (existing!=null)
            {
                var post = await _context.Posts.Where(x=>x.User.Id==userId).CountAsync();
                return new Response<int>(post);
            }

            return new Response<int>(HttpStatusCode.NotFound, "not found");

        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, "error in server");
        }
    }

    public async Task<Response<int>> GetFollowing(string userId)
    {

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user!=null)
            {
                var userFollow = await _context.FollowingRelationShips.Where(x => x.UserId == userId).CountAsync();
                return new Response<int>(userFollow);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "not found");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> GetFollowers(string userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user!=null)
            {
                var userFollow = await _context.FollowingRelationShips.Where(x => x.FollowingId == userId).CountAsync();
                return new Response<int>(userFollow);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "not found");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}