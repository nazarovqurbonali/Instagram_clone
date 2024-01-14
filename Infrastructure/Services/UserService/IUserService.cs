using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Filters;
using Domain.Filters.UserFilter;
using Domain.Responses;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<PagedResponse<List<GetUserDto>>> GetUsers(UserFilter filter);
    /*Task<Response<GetUserDto>> GetUserById(string userId);
    Task<Response<GetUserDto>> UpdateUser(AddUserDto addUser);*/
    Task<Response<bool>> AddSearchHistory(AddSearchHistoryDto searchHistory, string userId);
    Task<Response<List<GetSearchHistoryDto>>> GetSearchHistories(string userId);
    Task<Response<bool>> DeleteSearchHistory(int id);
    Task<Response<bool>> DeleteSearchHistories(string userId);
    Task<Response<bool>> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory, string userId);
    Task<Response<List<GetUserSearchHistoryDto>>> GetUserSearchHistories(string userId);
    Task<Response<bool>> DeleteUserSearchHistory(int id);
    Task<Response<bool>> DeleteUserSearchHistories(string userId);
    Task<Response<bool>> DeleteUser(string id);
}