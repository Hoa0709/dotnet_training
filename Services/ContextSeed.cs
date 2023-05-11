using app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.superadmin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.manager.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.staff.ToString()));
    }
    public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new AppUser
        {
            UserName = "superadmin",
            Email = "superadmin@gmail.com",
            FullName = "Mukesh",
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                await userManager.AddToRoleAsync(defaultUser, Roles.superadmin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.manager.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.staff.ToString());
            }
        }
    }
}
