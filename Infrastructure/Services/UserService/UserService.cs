using System.Net;
using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Entities;
using Domain.Filters.UserFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Infrastructure.Services.UserService;

public class UserService(DataContext context)
    : IUserService
{
    public async Task<PagedResponse<List<GetUserDto>>> GetUsers(UserFilter filter)
    {
        try
        {
            var users = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.UserName))
                users = users.Where(u => u.UserName!.ToLower().Contains(filter.UserName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                users = users.Where(u => u.Email!.ToLower().Contains(filter.Email.ToLower()));
            var result = await (from u in users
                    select new GetUserDto()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Avatar = u.UserProfile.Image,
                        FullName = u.UserProfile.FirstName + " " + u.UserProfile.LastName,
                        SubscribersCount = context.FollowingRelationShips.Count(x => x.FollowingId == u.Id)
                    })
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking().ToListAsync();
            var totalRecord = users.Count();

            return new PagedResponse<List<GetUserDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUserDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> AddSearchHistory(AddSearchHistoryDto searchHistory, string userId)
    {
        try
        {
            var existSearchHistory =
                await context.SearchHistories.FirstOrDefaultAsync(x => x.UserId == userId && x.Text == searchHistory.Text);
            if (existSearchHistory != null)
            {
                existSearchHistory.SearchDate = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            var history = new SearchHistory()
            {
                UserId = userId,
                Text = searchHistory.Text,
                SearchDate = DateTime.UtcNow
            };
            await context.SearchHistories.AddAsync(history);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetSearchHistoryDto>>> GetSearchHistories(string id)
    {
        try
        {
            var result = await (from s in context.SearchHistories
                where s.UserId == id
                orderby s.SearchDate descending
                select new GetSearchHistoryDto()
                {
                    Id = s.Id,
                    Text = s.Text
                })
                .AsNoTracking().ToListAsync();
            return new Response<List<GetSearchHistoryDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetSearchHistoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSearchHistory(int id)
    {
        try
        {
            var searchHistory = await context.SearchHistories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (searchHistory == null) return new Response<bool>(HttpStatusCode.NotFound, "Search history not found!");
            context.SearchHistories.Remove(searchHistory);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSearchHistories(string userId)
    {
        try
        {
            var searchHistories = await context.SearchHistories.Where(x => x.UserId == userId).ToListAsync();
            context.SearchHistories.RemoveRange(searchHistories);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<bool>> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory, string userId)
    {
        try
        {
            var existUser = await context.UserSearchHistories.FirstOrDefaultAsync(x =>
                x.UserId == userId && x.UserSearchId == userSearchHistory.UserSearchId);
            if (existUser != null)
            {
                existUser.SearchDate = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return new Response<bool>(true);
            } 
            var user = new UserSearchHistory()
            {
                UserId = userId,
                UserSearchId = userSearchHistory.UserSearchId,
                SearchDate = DateTime.UtcNow
            };
            await context.UserSearchHistories.AddAsync(user);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetUserSearchHistoryDto>>> GetUserSearchHistories(string userId)
    {
        try
        {
            var result = await (from u in context.UserSearchHistories
                    where u.UserId == userId
                    orderby u.SearchDate descending 
                    select new GetUserSearchHistoryDto()
                    {
                        Id = u.Id,
                        Users = new GetUserDto()
                        {
                            Id = u.UserSearch.Id,
                            UserName = u.UserSearch.UserName,
                            Avatar = u.UserSearch.UserProfile.Image,
                            FullName = u.UserSearch.UserProfile.FirstName + " " + u.UserSearch.UserProfile.LastName,
                            SubscribersCount =
                                context.FollowingRelationShips.Count(x => x.FollowingId == u.UserSearch.Id)
                        }
                    })
                .AsNoTracking().ToListAsync();
            return new Response<List<GetUserSearchHistoryDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserSearchHistoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserSearchHistory(int id)
    {
        try
        {
            var userSearchHistory = await context.UserSearchHistories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (userSearchHistory == null)
                return new Response<bool>(HttpStatusCode.NotFound, "User search history not found!");
            context.UserSearchHistories.Remove(userSearchHistory);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserSearchHistories(string userId)
    {
        try
        {
            var userSearchHistories = await context.UserSearchHistories.Where(x => x.UserId == userId).ToListAsync();
            context.UserSearchHistories.RemoveRange(userSearchHistories);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /*public async Task<Response<GetUserDto>> GetUserById(string userId)
    {
        try
        {
            var user = await (from u in context.Users
                where u.Id == userId
                select new GetUserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Avatar = u.UserProfile.Image,
                    UserType = u.UserType,
                    DateRegistered = u.DateRegistred
                }).AsNoTracking().FirstOrDefaultAsync();
            return new Response<GetUserDto>(user);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetUserDto>> UpdateUser(AddUserDto addUser)
    {
        try
        {
            var user = mapper.Map<User>(addUser);
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, "Yor account updated successfully");
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }*/

    public async Task<Response<bool>> DeleteUser(string id)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return new Response<bool>(HttpStatusCode.BadRequest, "User not found");
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}