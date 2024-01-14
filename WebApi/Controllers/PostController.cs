using System.Net;
using Domain.Dtos.PostCommentDto;
using Domain.Dtos.PostDto;
using Domain.Dtos.PostFavoriteDto;
using Domain.Filters;
using Domain.Filters.PostFilter;
using Domain.Responses;
using Infrastructure.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PostController(IPostService service) : BaseController
{
    [HttpGet("get-posts")]
    public async Task<IActionResult> GetPosts([FromQuery] PostFilter filter)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sid")!.Value;
        var result = await service.GetPosts(filter, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-reels")]
    public async Task<IActionResult> GetReels(PaginationFilter filter)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.GetReels(filter, userId);
        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("get-post-by-id")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var userId = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        var result = await service.GetPostById(id, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-following-post")]
    public async Task<IActionResult> GetFollowingPost([FromQuery] PostFollowingFilter filter)
    {
        var userId = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        var result = await service.GetPostByFollowing(filter, userId);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("add-post")]
    public async Task<IActionResult> AddPost([FromForm] AddPostDto post)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
            var result = await service.AddPost(post, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<PostDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-post")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await service.DeletePost(id);
        return StatusCode(result.StatusCode, result);
    }


    [HttpPost("like-post")]
    public async Task<IActionResult> LikePost(int postId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
        var result = await service.LikePost(userId, postId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("view-post")]
    public async Task<IActionResult> ViewPost(int postId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
        var result = await service.ViewPost(userId, postId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] AddPostCommentDto comment)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "sid")!.Value;
            var result = await service.AddComment(comment, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-comment")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var result = await service.DeleteComment(commentId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-post-favorite")]
    public async Task<IActionResult> AddPostFavorite([FromBody] AddPostFavoriteDto postFavorite)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
            var result = await service.AddPostFavorite(postFavorite, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<PostFavoriteDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}