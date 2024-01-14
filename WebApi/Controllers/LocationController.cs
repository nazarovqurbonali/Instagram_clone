using System.Net;
using Domain.Dtos.LocationDto;
using Domain.Filters.LocationFilter;
using Domain.Responses;
using Infrastructure.Services.LocationDto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class LocationController : BaseController
{
    private readonly ILocationService _service;

    public LocationController(ILocationService service)
    {
        _service = service;
    }

    [HttpGet("get-Locations")]
    public async Task<IActionResult> GetLocations([FromQuery]LocationFilter filter)
    {
        var result = await _service.GetLocations(filter);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-Location-by-id")]
    public async Task<IActionResult> GetLocationById(int id)
    {
        var result = await _service.GetLocationById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-Location")]
    public async Task<IActionResult> AddLocation([FromBody]AddLocationDto location)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.AddLocation(location);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPut("update-Location")]
    public async Task<IActionResult> UpdateLocation([FromBody]UpdateLocationDto location)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateLocation(location);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-Location")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var result = await _service.DeleteLocation(id);
        return StatusCode(result.StatusCode, result);
    }
}