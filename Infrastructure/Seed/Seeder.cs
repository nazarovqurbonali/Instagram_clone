using Domain.Entities;
using Domain.Entities.User;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder(DataContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task SeedRole()
    {
        var newRoles = new List<IdentityRole>()
        {
            new(Roles.Admin),
            new(Roles.User)
        };

        var existing = roleManager.Roles.ToList();
        foreach (var role in newRoles)
        {
            if (existing.Exists(e => e.Name == role.Name) == false)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
    
    public async Task SeedLocation()
    {
        var locations = await context.Locations.FindAsync(1);
        if (locations != null) return;
        var location = new Location()
        {
            LocationId = 1,
            City = "",
            Country = "",
            State = "",
            ZipCode = ""
        };
        await context.Locations.AddAsync(location);
        await context.SaveChangesAsync();
    }

    public async Task SeedUser()
    {
        var existing = await userManager.FindByNameAsync("admin");
        if (existing != null) return;
        var identity = new User()
        {
            UserName = "admin",
            PhoneNumber = "+992005442641",
            Email = "admin@gmail.com",
            DateRegistred = DateTime.UtcNow,
        };
        await userManager.CreateAsync(identity, "hello123");
        await userManager.AddToRoleAsync(identity, Roles.Admin);

        var profileAdmin = new UserProfile()
        {
            UserId = identity.Id,
            FirstName = "Shodmon",
            LastName = "Inoyatzoda",
            Occupation = string.Empty,
            DateUpdated = DateTime.UtcNow,
            LocationId = 1,
            Dob = DateTime.UtcNow,
            Image = string.Empty,
            About = string.Empty,
            Gender = Gender.Male,
        };
        await context.UserProfiles.AddAsync(profileAdmin);
        await context.SaveChangesAsync();
    }
}

public class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}