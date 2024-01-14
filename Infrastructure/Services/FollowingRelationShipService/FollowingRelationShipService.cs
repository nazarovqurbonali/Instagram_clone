using System.Net;
using AutoMapper;
using Domain.Dtos.FollowingRelationshipDto;
using Domain.Dtos.UserDto;
using Domain.Entities.User;
using Domain.Filters.FollowingRelationShipFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.FollowingRelationShipService;

public class FollowingRelationShipService(DataContext context, IMapper mapper) : IFollowingRelationShipService
{
    public async Task<Response<GetFollowingRelationShipDto>> GetFollowingRelationShip(
        FollowingRelationShipFilter filter)
    {
        try
        {
            var followingRelationShips = context.FollowingRelationShips.AsQueryable();
            var response = await (from f in followingRelationShips
                select new GetFollowingRelationShipDto()
                {
                    Subscribers = (from fr in followingRelationShips
                        where fr.FollowingId == filter.UserId
                        select new SubscribersDto()
                        {
                            Id = fr.FollowingRelationShipId,
                            UserShortInfo = new GetUserShortInfoDto()
                            {
                                UserId = fr.UserId,
                                UserName = fr.User.UserName,
                                Fullname = (fr.User.UserProfile.FirstName + " " + fr.User.UserProfile.LastName),
                                UserPhoto = fr.User.UserProfile.Image
                            }
                        }).ToList(),
                    Subscriptions = (from fr in followingRelationShips
                        where fr.UserId == filter.UserId
                        select new SubscriptionsDto()
                        {
                            Id = fr.FollowingRelationShipId,
                            UserShortInfo = new GetUserShortInfoDto()
                            {
                                UserId = fr.FollowingId,
                                UserName = fr.Following.UserName,
                                Fullname =
                                    (fr.Following.UserProfile.FirstName + " " + fr.Following.UserProfile.LastName),
                                UserPhoto = fr.Following.UserProfile.Image
                            }
                        }).ToList()
                }).AsNoTracking().FirstOrDefaultAsync();
            var totalRecord = followingRelationShips.Count();
            return new Response<GetFollowingRelationShipDto>(response);
        }
        catch (Exception e)
        {
            return new Response<GetFollowingRelationShipDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<SubscribersDto>>> GetSubscribers(FollowingRelationShipFilter filter)
    {
        try
        {
            var subscribers = await (from fr in context.FollowingRelationShips
                where fr.FollowingId == filter.UserId
                select new SubscribersDto()
                {
                    Id = fr.FollowingRelationShipId,
                    UserShortInfo = new GetUserShortInfoDto()
                    {
                        UserId = fr.UserId,
                        UserName = fr.User.UserName,
                        Fullname = (fr.User.UserProfile.FirstName + " " + fr.User.UserProfile.LastName),
                        UserPhoto = fr.User.UserProfile.Image
                    }
                }).AsNoTracking().ToListAsync();
            return new Response<List<SubscribersDto>>(subscribers);
        }
        catch (Exception e)
        {
            return new Response<List<SubscribersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<SubscriptionsDto>>> GetSubscriptions(FollowingRelationShipFilter filter)
    {
        try
        {
            var subscriptions = await (from fr in context.FollowingRelationShips
                where fr.UserId == filter.UserId
                select new SubscriptionsDto()
                {
                    Id = fr.FollowingRelationShipId,
                    UserShortInfo = new GetUserShortInfoDto()
                    {
                        UserId = fr.FollowingId,
                        UserName = fr.Following.UserName,
                        Fullname =
                            (fr.Following.UserProfile.FirstName + " " + fr.Following.UserProfile.LastName),
                        UserPhoto = fr.Following.UserProfile.Image
                    }
                }).AsNoTracking().ToListAsync();
            return new Response<List<SubscriptionsDto>>(subscriptions);
        }
        catch (Exception e)
        {
            return new Response<List<SubscriptionsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> AddFollowingRelationShip(string followingUserId, string userId)
    {
        try
        {
            if (followingUserId == userId)
                return new Response<bool>(HttpStatusCode.BadRequest, "You will not be able to subscribe to yourself");
            var user = await context.Users.FindAsync(userId);
            var followingUser = await context.Users.FindAsync(followingUserId);
            if (user == null || followingUser == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "User not found");
            var following = new FollowingRelationShip()
            {
                UserId = userId,
                FollowingId = followingUserId,
                DateFollowed = DateTime.UtcNow
            };
            await context.FollowingRelationShips.AddAsync(following);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteFollowingRelationShip(int id)
    {
        try
        {
            var following =
                await context.FollowingRelationShips.FindAsync(id);
            if (following == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Following relationShip not found");
            context.FollowingRelationShips.Remove(following);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}