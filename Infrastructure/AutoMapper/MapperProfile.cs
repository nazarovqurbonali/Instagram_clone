using AutoMapper;
using Domain.Dtos.CategoryDto;
using Domain.Dtos.ExternalAccountDto;
using Domain.Dtos.FollowingRelationshipDto;
using Domain.Dtos.LocationDto;
using Domain.Dtos.PostCategoryDto;
using Domain.Dtos.PostCommentDto;
using Domain.Dtos.PostDto;
using Domain.Dtos.PostFavoriteDto;
using Domain.Dtos.PostStatDto;
using Domain.Dtos.PostTagDto;
using Domain.Dtos.StoryDtos;
using Domain.Dtos.StoryViewDtos;
using Domain.Dtos.TagDto;
using Domain.Dtos.UserDto;
using Domain.Dtos.UserProfileDto;
using Domain.Dtos.UserSettingDto;
using Domain.Entities;
using Domain.Entities.Post;
using Domain.Entities.User;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<ExternalAccount, ExternalAccountDto>().ReverseMap();

        CreateMap<StoryView,GetStoryViewDto>();

        CreateMap<AddStoryDto, Story>();
        CreateMap<Story, GetStoryDto>();

        CreateMap<FollowingRelationShip, GetFollowingRelationShipDto>();
        CreateMap<AddFollowingRelationShipDto, FollowingRelationShip>()
            .ForMember(dest => dest.DateFollowed, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Location, GetLocationDto>();
        CreateMap<AddLocationDto, Location>();

        CreateMap<PostComment, GetPostCommentDto>();
        CreateMap<AddPostCommentDto, PostComment>()
            .ForMember(dest => dest.DateCommented, opt => opt.MapFrom(src => DateTime.UtcNow));

        // CreateMap<Post, GetPostDto>()
        //     .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<AddPostDto, Post>()
            .ForMember(dest => dest.DatePublished, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Images, opt => opt.Ignore());

        CreateMap<PostFavorite, GetPostFavoriteDto>();
        CreateMap<AddPostFavoriteDto, PostFavorite>();

        CreateMap<PostLike, PostStatDto>().ReverseMap();

        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.DateRegistred, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UserProfile, GetUserProfileDto>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.ToShortDateString()))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        CreateMap<AddUserProfileDto, UserProfile>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.ToShortDateString()))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.FileName))
            .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UserSetting, UserSettingDto>().ReverseMap();

        CreateMap<PostCategoryDto, PostCategory>().ReverseMap();

    }
}