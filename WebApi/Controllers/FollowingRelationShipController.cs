using System.Net;
using Domain.Dtos.FollowingRelationshipDto;
using Domain.Filters.FollowingRelationShipFilter;
using Domain.Responses;
using Infrastructure.Services.FollowingRelationShipService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class FollowingRelationShipController(IFollowingRelationShipService service) : BaseController
{
    /*[HttpGet("get-following-relation-ship")]
    public async Task<IActionResult> GetFollowingRelationShips(FollowingRelationShipFilter filter)
    {
        var result = await service.GetFollowingRelationShip(filter);
        return StatusCode(result.StatusCode, result);
    }*/

    [HttpGet("get-subscribers")]
    public async Task<IActionResult> GetSubscribers(FollowingRelationShipFilter filter)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetSubscribers(filter);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<List<SubscribersDto>>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("get-subscriptions")]
    public async Task<IActionResult> GetSubscriptions(FollowingRelationShipFilter filter)
    {
        if (ModelState.IsValid)
        {
            var result = await service.GetSubscriptions(filter);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<List<SubscriptionsDto>>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("add-following-relation-ship")]
    public async Task<IActionResult> AddFollowingRelationShip(string followingUserId)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "sid")!.Value;
            var result = await service.AddFollowingRelationShip(followingUserId, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<FollowingRelationShipDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-following-relation-ship")]
    public async Task<IActionResult> DeleteFollowingRelationShip(int id)
    {
        var result = await service.DeleteFollowingRelationShip(id);
        return StatusCode(result.StatusCode, result);
    }
}