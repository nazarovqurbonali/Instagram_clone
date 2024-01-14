// using System.Net;
// using Domain.Dtos.UserSettingDto;
// using Domain.Filters;
// using Domain.Responses;
// using Infrastructure.Services.UserSettingService;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApi.Controllers;
//
// public class UserSettingController : BaseController
// {
//     private readonly IUserSettingService _service;
//
//     public UserSettingController(IUserSettingService service)
//     {
//         _service = service;
//     }
//
//     [HttpGet("get-user-settings")]
//     public async Task<IActionResult> GetUserSettings([FromQuery]PaginationFilter filter)
//     {
//         var result = await _service.GetUserSettings(filter);
//         return StatusCode(result.StatusCode, result);
//     }
//
//     [HttpGet("get-user-setting-by-id")]
//     public async Task<IActionResult> GetUserSettingById(int id)
//     {
//         var result = await _service.GetUserSettingById(id);
//         return StatusCode(result.StatusCode, result);
//     }
//
//     [HttpPost("add-user-setting")]
//     public async Task<IActionResult> AddUserSetting([FromBody]UserSettingDto userSetting)
//     {
//         if (ModelState.IsValid)
//         {
//             var result = await _service.AddUserSetting(userSetting);
//             return StatusCode(result.StatusCode, result);
//         }
//
//         var errors = ModelState.SelectMany(e => e.Value.Errors.Select(er => er.ErrorMessage)).ToList();
//         var response = new Response<UserSettingDto>(HttpStatusCode.BadRequest, errors);
//         return StatusCode(response.StatusCode, response);
//     }
//     
//     [HttpPut("update-user-setting")]
//     public async Task<IActionResult> UpdateUserSetting([FromBody]UserSettingDto userSetting)
//     {
//         if (ModelState.IsValid)
//         {
//             var result = await _service.UpdateUserSetting(userSetting);
//             return StatusCode(result.StatusCode, result);
//         }
//
//         var errors = ModelState.SelectMany(e => e.Value.Errors.Select(er => er.ErrorMessage)).ToList();
//         var response = new Response<UserSettingDto>(HttpStatusCode.BadRequest, errors);
//         return StatusCode(response.StatusCode, response);
//     }
//
//     [HttpDelete("delete-user-setting")]
//     public async Task<IActionResult> DeleteUserSetting(int id)
//     {
//         var result = await _service.DeleteUserSetting(id);
//         return StatusCode(result.StatusCode, result);
//     }
// }