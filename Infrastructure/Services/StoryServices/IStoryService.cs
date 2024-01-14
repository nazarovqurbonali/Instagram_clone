using Domain.Dtos.StoryDtos;
using Domain.Responses;

namespace Infrastructure.Services.StoryServices;

public interface IStoryService
{
   Task<Response<List<GetStoryDto>>> GetStories(string userId, string userTokenId);
   Task<Response<GetStoryDto>> GetStoryById(int id,string token,string userName);
   Task<Response<GetStoryDto>> AddStory(AddStoryDto file,string token);
   Task<Response<string>> StoryLike(int storyId,string userId);
   Task<Response<bool>> DeleteStory(int id);
}