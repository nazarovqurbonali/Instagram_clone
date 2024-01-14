using System.Net;
using Domain.Dtos.UserProfileDto;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserProfileService;

public class UserProfileService(DataContext context, IFileService fileService)
    : IUserProfileService
{
    public async Task<Response<GetUserProfileDto>> GetUserProfileById(string id)
    {
        try
        {
            var userProfile = await (from p in context.UserProfiles
                where p.UserId == id
                select new GetUserProfileDto()
                {
                    UserName = p.User.UserName!,
                    Gender = p.Gender.ToString()!,
                    Occupation = p.Occupation,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateUpdated = p.DateUpdated,
                    LocationId = p.LocationId,
                    Dob = p.Dob,
                    About = p.About,
                    Image = p.Image!,
                    PostCount = p.User.Posts.Count,
                    SubscribersCount = context.FollowingRelationShips.Count(x => x.FollowingId == id),
                    SubscriptionsCount = p.User.FollowingRelationShips.Count // context.FollowingRelationShips.Count(x => x.UserId == id)
                }).AsNoTracking().FirstOrDefaultAsync();
            
            if (userProfile != null)
            {
                
                return new Response<GetUserProfileDto>(userProfile);
            }
            return new Response<GetUserProfileDto>(HttpStatusCode.NotFound, "User not found");
        }
        catch (Exception e)
        {
            return new Response<GetUserProfileDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }


    #region UpdateUserProfile

    public async Task<Response<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto addUserProfile, string userId)
    {
        try
        {
            var existing = await context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            

            if (existing != null)
            {
                if (addUserProfile.FirstName != null) existing.FirstName = addUserProfile.FirstName;
                existing.FirstName = existing.FirstName;
                if (addUserProfile.LastName != null) existing.LastName = addUserProfile.LastName;
                existing.LastName = existing.LastName;
                if (addUserProfile.About != null) existing.About = addUserProfile.About;
                existing.About = existing.About;
                if (addUserProfile.Occupation != null) existing.Occupation = addUserProfile.Occupation;
                existing.Occupation = existing.Occupation;
                var loc = await context.Locations.FirstOrDefaultAsync(x => x.LocationId == addUserProfile.LocationId);
                if (loc == null)
                {
                    return new Response<GetUserProfileDto>(HttpStatusCode.NotFound, "not found this location");
                }
                else
                {
                    existing.LocationId = addUserProfile.LocationId;
                }

                if (addUserProfile.Gender != null) existing.Gender = addUserProfile.Gender;
                existing.Gender = existing.Gender;


                if (addUserProfile.Dob != null)
                {
                    existing.Dob = addUserProfile.Dob;
                }

                existing.Dob = addUserProfile.Dob;
                
                existing.DateUpdated = DateTime.UtcNow;
                
                if (existing.Image != null)
                {
                    if (addUserProfile != null && existing.Image != null)
                    {
                        fileService.DeleteFile(existing.Image);
                        existing.Image = fileService.CreateFile(addUserProfile.Image).Data;
                        await context.SaveChangesAsync();
                    }
                    else if (addUserProfile.Image == null)
                    {
                        existing.Image = existing.Image;
                        await context.SaveChangesAsync();
                    }
                }
                else if (existing.Image == null && addUserProfile.Image != null)
                {
                    existing.Image = fileService.CreateFile(addUserProfile.Image).Data;
                    await context.SaveChangesAsync();
                }


                await context.SaveChangesAsync();
                var mapped = new GetUserProfileDto()
                {
                    About = existing.About,
                    Image = existing.Image,
                    Occupation = existing.Occupation,
                    FirstName = existing.FirstName,
                    LastName = existing.LastName,
                    DateUpdated = existing.DateUpdated,
                    LocationId = existing.LocationId,
                    Dob = existing.Dob,
                    Gender = (string)existing.Gender.ToString()
                };

                return new Response<GetUserProfileDto>(mapped);
            }

            return new Response<GetUserProfileDto>(HttpStatusCode.BadRequest, "not found");
        }
        catch (Exception e)
        {
            return new Response<GetUserProfileDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion
}