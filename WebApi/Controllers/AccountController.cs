using System.Net;
using Domain.Dtos;
using Domain.Dtos.EmailDto;
using Domain.Dtos.LoginDto;
using Domain.Dtos.RegisterDto;
using Domain.Responses;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController(IAccountService service) : BaseController
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody]RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await service.Register(model);
            return StatusCode(response.StatusCode, response);
        }
        else
        {
            var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await service.Login(model);
            return StatusCode(response.StatusCode, response);
        }
        else
        {
            var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
            return StatusCode(response.StatusCode, response);
        }
    }
    
    [HttpDelete("ForgotPassword")]
    [AllowAnonymous]
    public async Task<Response<string>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        return await service.ForgotPasswordTokenGenerator(forgotPasswordDto);
    }
    
      
    [HttpDelete("ResetPassword")]
    [AllowAnonymous]
    public async Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        return await service.ResetPassword(resetPasswordDto);
    }
    
    [HttpPut("ChangePassword")]
    [AllowAnonymous]
    public async Task<Response<string>> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
        return await service.ChangePassword(changePasswordDto,userId!);
    }
}