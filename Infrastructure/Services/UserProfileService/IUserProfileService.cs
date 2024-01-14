using Domain.Dtos.UserProfileDto;
using Domain.Filters.UserProfileFilter;
using Domain.Responses;

namespace Infrastructure.Services.UserProfileService;

public interface IUserProfileService
{
    Task<Response<GetUserProfileDto>> GetUserProfileById(string userid);
    Task<Response<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto addUserProfile,string userid);
}