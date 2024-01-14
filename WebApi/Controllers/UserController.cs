using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Filters.UserFilter;
using Domain.Responses;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserController(IUserService service) : BaseController
{
    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers([FromQuery]UserFilter filter)
    {
        var result = await service.GetUsers(filter);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-search-history")]
    public async Task<IActionResult> AddSearchHistory(AddSearchHistoryDto searchHistory)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await service.AddSearchHistory(searchHistory, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("get-search-histories")]
    public async Task<IActionResult> GetSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.GetSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-search-history")]
    public async Task<IActionResult> DeleteSearchHistory([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteSearchHistory(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-search-histories")]
    public async Task<IActionResult> DeleteSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.DeleteSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("add-user-search-history")]
    public async Task<IActionResult> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await service.AddUserSearchHistory(userSearchHistory, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("get-user-search-histories")]
    public async Task<IActionResult> GetUserSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.GetUserSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-user-search-history")]
    public async Task<IActionResult> DeleteUserSearchHistory([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await service.DeleteUserSearchHistory(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-user-search-histories")]
    public async Task<IActionResult> DeleteUserSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.DeleteUserSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }

    /*[HttpGet("get-User-by-id")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var result = await _service.GetUserById(userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPut("update-User")]
    public async Task<IActionResult> UpdateUser([FromBody]AddUserDto user)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateUser(user);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value?
            .Errors.Select(er => er.ErrorMessage) ?? Array.Empty<string>()).ToList();
        var response = new Response<UserDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }*/

    [HttpDelete("delete-user")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await service.DeleteUser(userId);
        return StatusCode(result.StatusCode, result);
    }
}