using System.Net;
using AutoMapper;
using Domain.Dtos.PostCommentDto;
using Domain.Dtos.PostDto;
using Domain.Dtos.PostFavoriteDto;
using Domain.Dtos.UserDto;
using Domain.Entities.Post;
using Domain.Filters;
using Domain.Filters.PostCommentFilter;
using Domain.Filters.PostFilter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;
using static System.Guid;

namespace Infrastructure.Services.PostService;

public class PostService(DataContext context, IMapper mapper, IFileService fileService)
    : IPostService
{
    public async Task<PagedResponse<List<GetPostDto>>> GetPosts(PostFilter filter, string userId)
    {
        try
        {
            var posts = context.Posts.AsQueryable();
            if (filter.UserId != null)
                posts = posts.Where(p => p.UserId == filter.UserId);
            if (!string.IsNullOrEmpty(filter.Content))
                posts = posts.Where(p => p.Content!.ToLower().Contains(filter.Content.ToLower()));
            if (!string.IsNullOrEmpty(filter.Title))
                posts = posts.Where(p => p.Title!.ToLower().Contains(filter.Title.ToLower()));
            var result = await (from p in posts
                    select new GetPostDto()
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        Title = p.Title,
                        Content = p.Content,
                        DatePublished = p.DatePublished,
                        Images = p.Images.Select(i => i.ImageName).ToList(),
                        PostLike = p.PostLike.PostUserLikes.Any(l => l.UserId == userId && l.PostLikeId == p.PostId),
                        PostLikeCount = p.PostLike.LikeCount,
                        UserLikes = p.UserId == userId
                            ? p.PostLike.PostUserLikes.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        PostView = p.PostView.ViewCount,
                        UserViews = p.UserId == userId
                            ? p.PostView.PostViewUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        CommentCount = p.PostComments.Count(),
                        PostFavorite =
                            p.PostFavorite.PostFavoriteUsers.Any(
                                l => l.UserId == userId && l.PostFavoriteId == p.PostId),
                        UserFavorite = p.UserId == userId
                            ? p.PostFavorite.PostFavoriteUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        Comments = p.PostComments.Select(s => new GetPostCommentDto()
                        {
                            PostCommentId = s.PostCommentId,
                            UserId = s.UserId,
                            Comment = s.Comment,
                            DateCommented = s.DateCommented
                        }).OrderByDescending(c => c.DateCommented).ToList(),
                    })
                .OrderBy(x => NewGuid())
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking().ToListAsync();
            var totalRecord = posts.Count();
            return new PagedResponse<List<GetPostDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPostDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetReelsDto>>> GetReels(PaginationFilter filter, string userId)
    {
        try
        {
            var posts = context.Posts.AsQueryable();
            var totalRecord = posts.Count();
            var result = await (from p in posts
                    where p.Images.Any(x => x.ImageName.Contains(".mp4")) ||
                          p.Images.Any(x => x.ImageName.Contains(".avi")) ||
                          p.Images.Any(x => x.ImageName.Contains(".mpg")) ||
                          p.Images.Any(x => x.ImageName.Contains(".3pg"))
                    select new GetReelsDto()
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        Title = p.Title,
                        Content = p.Content,
                        DatePublished = p.DatePublished,
                        Images = p.Images.Where(x =>
                            x.ImageName.ToLower().Contains(".mp4") ||
                            x.ImageName.ToLower().Contains(".avi") ||
                            x.ImageName.ToLower().Contains(".mpg") ||
                            x.ImageName.ToLower().Contains(".3gp")).Select(i => i.ImageName).FirstOrDefault(),
                        PostLike = p.PostLike.PostUserLikes.Any(l => l.UserId == userId && l.PostLikeId == p.PostId),
                        PostLikeCount = p.PostLike.LikeCount,
                        UserLikes = p.UserId == userId
                            ? p.PostLike.PostUserLikes.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        PostView = p.PostView.ViewCount,
                        UserViews = p.UserId == userId
                            ? p.PostView.PostViewUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        CommentCount = p.PostComments.Count(),
                        PostFavorite =
                            p.PostFavorite.PostFavoriteUsers.Any(
                                l => l.UserId == userId && l.PostFavoriteId == p.PostId),
                        UserFavorite = p.UserId == userId
                            ? p.PostFavorite.PostFavoriteUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        Comments = p.PostComments.Select(s => new GetPostCommentDto()
                        {
                            PostCommentId = s.PostCommentId,
                            UserId = s.UserId,
                            Comment = s.Comment,
                            DateCommented = s.DateCommented
                        }).OrderByDescending(c => c.DateCommented).ToList()
                    })
                .OrderBy(x => NewGuid())
                .Skip(int.Abs(filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).AsNoTracking().ToListAsync();
            return new PagedResponse<List<GetReelsDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetReelsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetPostDto>> GetPostById(int id, string userId)
    {
        try
        {
            var post = await (from p in context.Posts
                where p.PostId == id
                select new GetPostDto()
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    Title = p.Title,
                    Content = p.Content,
                    DatePublished = p.DatePublished,
                    Images = p.Images.Select(i => i.ImageName).ToList(),
                    PostLike = p.PostLike.PostUserLikes.Any(l => l.UserId == userId && l.PostLikeId == p.PostId),
                    PostLikeCount = p.PostLike.LikeCount,
                    UserLikes = p.UserId == userId
                        ? p.PostLike.PostUserLikes.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    PostView = p.PostView.ViewCount,
                    UserViews = p.UserId == userId
                        ? p.PostView.PostViewUsers.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    CommentCount = p.PostComments.Count(),
                    PostFavorite =
                        p.PostFavorite.PostFavoriteUsers.Any(l => l.UserId == userId && l.PostFavoriteId == p.PostId),
                    UserFavorite = p.UserId == userId
                        ? p.PostFavorite.PostFavoriteUsers.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    Comments = p.PostComments.Select(s => new GetPostCommentDto()
                    {
                        PostCommentId = s.PostCommentId,
                        UserId = s.UserId,
                        Comment = s.Comment,
                        DateCommented = s.DateCommented
                    }).OrderByDescending(c => c.DateCommented).ToList(),
                }).AsNoTracking().FirstOrDefaultAsync(p => p.PostId == id);
            return new Response<GetPostDto>(post);
        }
        catch (Exception e)
        {
            return new Response<GetPostDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetPostDto>>> GetPostByFollowing(PostFollowingFilter filter, string userId)
    {
        try
        {
            var posts = await (from p in context.Posts
                join f in context.FollowingRelationShips on p.UserId equals f.FollowingId
                where f.UserId == filter.UserId
                select new GetPostDto()
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    Title = p.Title,
                    Content = p.Content,
                    DatePublished = p.DatePublished,
                    Images = p.Images.Select(i => i.ImageName).ToList(),
                    PostLike = p.PostLike.PostUserLikes.Any(l => l.UserId == userId && l.PostLikeId == p.PostId),
                    PostLikeCount = p.PostLike.LikeCount,
                    UserLikes = p.UserId == userId
                        ? p.PostLike.PostUserLikes.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    PostView = p.PostView.ViewCount,
                    UserViews = p.UserId == userId
                        ? p.PostView.PostViewUsers.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    CommentCount = p.PostComments.Count(),
                    PostFavorite =
                        p.PostFavorite.PostFavoriteUsers.Any(l => l.UserId == userId && l.PostFavoriteId == p.PostId),
                    UserFavorite = p.UserId == userId
                        ? p.PostFavorite.PostFavoriteUsers.Select(u => new GetUserShortInfoDto()
                        {
                            UserId = u.UserId,
                            UserName = u.User.UserName,
                            Fullname = string.Concat(u.User.UserProfile.FirstName + " " + u.User.UserProfile.LastName),
                            UserPhoto = u.User.UserProfile.Image
                        }).ToList()
                        : null,
                    Comments = p.PostComments.Select(s => new GetPostCommentDto()
                    {
                        PostCommentId = s.PostCommentId,
                        UserId = s.UserId,
                        Comment = s.Comment,
                        DateCommented = s.DateCommented
                    }).OrderByDescending(c => c.DateCommented).ToList(),
                }).AsNoTracking().ToListAsync();
            var totalRecord = posts.Count();
            return new PagedResponse<List<GetPostDto>>(posts, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPostDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<int>> AddPost(AddPostDto addPost, string userId)
    {
        try
        {
            var post = mapper.Map<Post>(addPost);
            post.UserId = userId;
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            var postLike = new PostLike()
            {
                PostId = post.PostId
            };
            var postView = new PostView()
            {
                PostId = post.PostId
            };
            var postFavorite = new PostFavorite()
            {
                PostId = post.PostId
            };
            var list = new List<Image>();
            foreach (var image in addPost.Images)
            {
                var imageName = fileService.CreateFile(image);
                var img = new Image()
                {
                    PostId = post.PostId,
                    ImageName = imageName.Data!
                };
                list.Add(img);
                // await context.Images.AddAsync(images);
                // await context.SaveChangesAsync();
            }

            await context.AddRangeAsync(list);

            await context.PostViews.AddAsync(postView);
            await context.PostLikes.AddAsync(postLike);
            await context.PostFavorites.AddAsync(postFavorite);
            await context.SaveChangesAsync();

            return new Response<int>(post.PostId);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeletePost(int id)
    {
        try
        {
            var post = await context.Posts.FindAsync(id);
            if (post == null) return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> LikePost(string userId, int postId)
    {
        try
        {
            var stats = await context.PostLikes.FirstOrDefaultAsync(e => e.PostId == postId);
            if (stats == null) return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");

            var existingStatUser =
                context.PostUserLikes.FirstOrDefault(st => st.UserId == userId && st.PostLikeId == stats.PostId);
            if (existingStatUser == null)
            {
                var newPostUserLike = new PostUserLike()
                {
                    UserId = userId,
                    PostLikeId = stats.PostId
                };
                await context.PostUserLikes.AddAsync(newPostUserLike);
                stats.LikeCount++;
                await context.SaveChangesAsync();
                return new Response<bool>(true);
            }

            context.PostUserLikes.Remove(existingStatUser);
            stats.LikeCount--;
            await context.SaveChangesAsync();
            return new Response<bool>(false);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> ViewPost(string userId, int postId)
    {
        try
        {
            var postViewUser =
                await context.PostViewUsers.FirstOrDefaultAsync(p => p.UserId == userId && p.PostViewId == postId);
            if (postViewUser != null) return new Response<bool>(true);

            var post = await context.PostViews.FindAsync(postId);
            post!.ViewCount++;
            var postView = new PostViewUser()
            {
                UserId = userId,
                PostViewId = postId
            };
            await context.PostViewUsers.AddAsync(postView);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetPostCommentDto>>> GetPostComments(PostCommentFilter filter)
    {
        try
        {
            var comments = context.PostComments.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Comment))
                comments = comments.Where(c => c.Comment.ToLower().Contains(filter.Comment.ToLower()));
            var response = await comments
                .Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).AsNoTracking().ToListAsync();
            var totalRecord = comments.Count();
            var mapped = mapper.Map<List<GetPostCommentDto>>(response);
            return new PagedResponse<List<GetPostCommentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPostCommentDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetPostCommentDto>> GetPostCommentById(int id)
    {
        try
        {
            var comment = await context.PostComments.FindAsync(id);
            var mapped = mapper.Map<GetPostCommentDto>(comment);
            return new Response<GetPostCommentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetPostCommentDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> AddComment(AddPostCommentDto addPostComment, string userId)
    {
        try
        {
            var post = await context.Posts.FindAsync(addPostComment.PostId);
            if (post == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");
            var comment = mapper.Map<PostComment>(addPostComment);
            comment.UserId = userId;
            await context.PostComments.AddAsync(comment);
            await context.SaveChangesAsync();
            var postCommentLike = new PostCommentLike() { PostCommentId = comment.PostCommentId };
            await context.PostCommentLikes.AddAsync(postCommentLike);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteComment(int commentId)
    {
        try
        {
            var comment = await context.PostComments.FindAsync(commentId);
            if (comment == null) return new Response<bool>(HttpStatusCode.BadRequest, "Comment not found");
            context.PostComments.Remove(comment);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetPostDto>>> GetPostFavorites(PaginationFilter filter, string userId)
    {
        try
        {
            var posts = context.Posts.AsQueryable();
            var result = await (from p in posts
                    select new GetPostDto()
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        Title = p.Title,
                        Content = p.Content,
                        DatePublished = p.DatePublished,
                        Images = p.Images.Select(i => i.ImageName).ToList(),
                        PostLike = p.PostLike.PostUserLikes.Any(l => l.UserId == userId && l.PostLikeId == p.PostId),
                        PostLikeCount = p.PostLike.LikeCount,
                        UserLikes = p.UserId == userId
                            ? p.PostLike.PostUserLikes.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        PostView = p.PostView.ViewCount,
                        UserViews = p.UserId == userId
                            ? p.PostView.PostViewUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        CommentCount = p.PostComments.Count(),
                        PostFavorite =
                            p.PostFavorite.PostFavoriteUsers.Any(
                                l => l.UserId == userId && l.PostFavoriteId == p.PostId),
                        UserFavorite = p.UserId == userId
                            ? p.PostFavorite.PostFavoriteUsers.Select(u => new GetUserShortInfoDto()
                            {
                                UserId = u.UserId,
                                UserName = u.User.UserName,
                                Fullname = string.Concat(u.User.UserProfile.FirstName + " " +
                                                         u.User.UserProfile.LastName),
                                UserPhoto = u.User.UserProfile.Image
                            }).ToList()
                            : null,
                        Comments = p.PostComments.Select(s => new GetPostCommentDto()
                        {
                            PostCommentId = s.PostCommentId,
                            UserId = s.UserId,
                            Comment = s.Comment,
                            DateCommented = s.DateCommented
                        }).OrderByDescending(c => c.DateCommented).ToList(),
                    })
                .Where(p => p.PostFavorite == true)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync();
            var totalRecord = posts.Count();
            return new PagedResponse<List<GetPostDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPostDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> AddPostFavorite(AddPostFavoriteDto addPostFavorite, string userId)
    {
        var favorite = await context.PostFavorites.FirstOrDefaultAsync(e => e.PostId == addPostFavorite.PostId);
        if (favorite == null) return new Response<bool>(HttpStatusCode.BadRequest, "Post not found");

        var existingFavoriteUser =
            context.PostFavoriteUsers.FirstOrDefault(st => st.UserId == userId && st.PostFavoriteId == favorite.PostId);
        if (existingFavoriteUser == null)
        {
            var newPostFavorite = new PostFavoriteUser()
            {
                UserId = userId,
                PostFavoriteId = favorite.PostId
            };
            await context.PostFavoriteUsers.AddAsync(newPostFavorite);
            favorite.FavoriteCount++;
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }

        context.PostFavoriteUsers.Remove(existingFavoriteUser);
        favorite.FavoriteCount--;
        await context.SaveChangesAsync();
        return new Response<bool>(false);
    }
}