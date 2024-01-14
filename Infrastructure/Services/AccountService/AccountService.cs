using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Domain.Dtos.EmailDto;
using Domain.Dtos.LoginDto;
using Domain.Dtos.MessagesDto;
using Domain.Dtos.RegisterDto;
using Domain.Entities.User;
using Domain.Enums;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;

namespace Infrastructure.Services.AccountService;

public class AccountService(IConfiguration configuration,
        UserManager<IdentityUser> userManager, DataContext dbContext, IEmailService emailService)
    : IAccountService
{
    public async Task<Response<string>> Register(RegisterDto model)
    {
        try
        {
            var result = await userManager.FindByNameAsync(model.UserName);
            if (result != null) return new Response<string>(HttpStatusCode.BadRequest, "Such a user already exists!");
            var user = new User()
            {
                UserName = model.UserName.ToLower(),
                Email = model.Email,
                UserType = UserType.Personal,
                DateRegistred = DateTime.UtcNow
            };
            var fullName = model.FullName.Split(' ');
            var profile = new UserProfile()
            {
                UserId = user.Id,
                FirstName = fullName[0],
                LastName = fullName[1] != string.Empty ? fullName[1] : string.Empty,
                Occupation = string.Empty,
                DateUpdated = DateTime.UtcNow,
                LocationId = 1,
                Dob = DateTime.UtcNow,
                Image = string.Empty,
                About = string.Empty,
                Gender = Gender.Male,
            };

            await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, Roles.User);
            await dbContext.UserProfiles.AddAsync(profile);
            await dbContext.SaveChangesAsync();
            return new Response<string>($"Done.  Your registered by id {user.Id}");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> Login(LoginDto model)
    {
        try
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, model.Password);
                if (result)
                    return new Response<string>(await GenerateJwtToken(user));
            }

            return new Response<string>(HttpStatusCode.BadRequest, "Your username or password is incorrect!!!");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var userProfile = await dbContext.UserProfiles.FindAsync(user.Id);
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sid, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, userProfile!.Image!),
        };
        //add roles
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        var securityTokenHandler = new JwtSecurityTokenHandler();
        var tokenString = securityTokenHandler.WriteToken(token);
        return tokenString;
    }

    public async Task<Response<string>> ChangePassword(ChangePasswordDto passwordDto, string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);

            var checkPassword = await userManager.CheckPasswordAsync(user!, passwordDto.OldPassword);
            if (checkPassword == false)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Password is incorrect");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user!);
            var result = await userManager.ResetPasswordAsync(user!, token, passwordDto.Password);
            if (result.Succeeded)
                return new Response<string>(HttpStatusCode.OK, "Success");
            return new Response<string>(HttpStatusCode.BadRequest, "Error, pleas try again.");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> ForgotPasswordTokenGenerator(ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            var existing = await userManager.FindByEmailAsync(forgotPasswordDto.Email!);
            if (existing == null) return new Response<string>(HttpStatusCode.BadRequest, "email  not found");
            var token = await userManager.GeneratePasswordResetTokenAsync(existing);
            var url = $"http://localhost:3000/account/reset-password?token={token}&email={forgotPasswordDto.Email}";
            emailService.SendEmail(
                new MessagesDto(new[] { forgotPasswordDto.Email }!, "Reset password",
                    $"<h1><a href=\"{url}\">Reset password</a></h1>"), TextFormat.Html);

            return new Response<string>(HttpStatusCode.OK, "reset password has been sent");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return new Response<string>(HttpStatusCode.BadRequest, "user not found");

            var resetPassResult =
                await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (resetPassResult.Succeeded)
                return new Response<string>(HttpStatusCode.OK, "success");

            return new Response<string>(HttpStatusCode.BadRequest, "please try again");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}