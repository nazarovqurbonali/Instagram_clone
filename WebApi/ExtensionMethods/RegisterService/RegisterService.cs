using Infrastructure.Data;
using Infrastructure.Seed;
using Infrastructure.Services;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.ChatService;
using Infrastructure.Services.ExternalAccountService;
using Infrastructure.Services.FileService;
using Infrastructure.Services.FollowingRelationShipService;
using Infrastructure.Services.LocationDto;
using Infrastructure.Services.LocationService;
using Infrastructure.Services.PostCommentService;
using Infrastructure.Services.PostFavoriteService;
using Infrastructure.Services.PostService;
using Infrastructure.Services.StatisticFollowAndPostService;
using Infrastructure.Services.StoryServices;
using Infrastructure.Services.StoryViewServices;
using Infrastructure.Services.UserProfileService;
using Infrastructure.Services.UserService;
using Infrastructure.Services.UserSettingService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExternalAccountService, ExternalAccountService>();
        services.AddScoped<IFollowingRelationShipService, FollowingRelationShipService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IPostCommentService, PostCommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IPostFavoriteService, PostFavoriteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserSettingService, UserSettingService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<Seeder>();
        services.AddScoped<IStatisticFollowAndPostService,StatisticFollowAndPostService>();    
        services.AddScoped<IStoryService, StoryService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<IStoryViewService, StoryViewService>();
        
        
        services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false; // must have at least one digit
                config.Password.RequireNonAlphanumeric = false; // must have at least one non-alphanumeric character
                config.Password.RequireUppercase = false; // must have at least one uppercase character
                config.Password.RequireLowercase = false;  // must have at least one lowercase character
            })
            //for registering usermanager and signinmanger
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
    }
}