using System.ComponentModel.DataAnnotations;
using Domain.Entities.Post;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.User;

public class User : IdentityUser
{
    public DateTime DateRegistred { get; set; }
    public UserType UserType { get; set; }
    public UserSetting UserSetting { get; set; } = null!;
    public UserProfile UserProfile { get; set; } = null!;
    public ExternalAccount ExternalAccount { get; set; } = null!;
    public List<StoryUser> StoryUsers { get; set; } = null!;
    public List<StoryLike> StoryLikes { get; set; } = null!;
    public List<FollowingRelationShip> FollowingRelationShips { get; set; } = null!;
    public List<Post.Post> Posts { get; set; } = null!;
    public List<PostComment> PostComments { get; set; } = null!;
    public List<PostFavorite> PostFavorites { get; set; } = null!;
    public List<PostUserLike> PostUserLikes { get; set; } = null!;
    public List<ListOfUserCommentLike> ListOfUserCommentLikes { get; set; } = null!;
}
