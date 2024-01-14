using Domain.Dtos.UserSettingDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.UserSettingService;

public interface IUserSettingService
{
    Task<PagedResponse<List<UserSettingDto>>> GetUserSettings(PaginationFilter filter);
    Task<Response<UserSettingDto>> GetUserSettingById(int id);
    Task<Response<UserSettingDto>> AddUserSetting(UserSettingDto addUserSetting);
    Task<Response<UserSettingDto>> UpdateUserSetting(UserSettingDto addUserSetting);
    Task<Response<bool>> DeleteUserSetting(int id);
}