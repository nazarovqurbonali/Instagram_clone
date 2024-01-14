using System.Net;
using AutoMapper;
using Domain.Dtos.UserSettingDto;
using Domain.Entities.User;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserSettingService;

public class UserSettingService(DataContext context, IMapper mapper) : IUserSettingService
{
    public async Task<PagedResponse<List<UserSettingDto>>> GetUserSettings(PaginationFilter filter)
    {
        try
        {
            var userSettings = context.UserSettings.AsQueryable();
            var response = await userSettings.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var mapped = mapper.Map<List<UserSettingDto>>(response);
            var totalRecord = userSettings.Count();
            return new PagedResponse<List<UserSettingDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<UserSettingDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<UserSettingDto>> GetUserSettingById(int id)
    {
        try
        {
            var userSetting = await context.UserSettings.FindAsync(id);
            var mapped = mapper.Map<UserSettingDto>(userSetting);
            return new Response<UserSettingDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<UserSettingDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<UserSettingDto>> AddUserSetting(UserSettingDto addUserSetting)
    {
        try
        {
            var userSetting = mapper.Map<UserSetting>(addUserSetting);
            await context.UserSettings.AddAsync(userSetting);
            await context.SaveChangesAsync();
            var mapped = mapper.Map<UserSettingDto>(userSetting);
            return new Response<UserSettingDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<UserSettingDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<UserSettingDto>> UpdateUserSetting(UserSettingDto addUserSetting)
    {
        try
        {
            var userSetting = mapper.Map<UserSetting>(addUserSetting);
            context.UserSettings.Update(userSetting);
            await context.SaveChangesAsync();
            var mapped = mapper.Map<UserSettingDto>(userSetting);
            return new Response<UserSettingDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<UserSettingDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserSetting(int id)
    {
        try
        {
            var userSetting = await context.UserSettings.FindAsync(id);
            if (userSetting == null) return new Response<bool>(HttpStatusCode.BadRequest, "User setting not found");
            context.UserSettings.Remove(userSetting);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}