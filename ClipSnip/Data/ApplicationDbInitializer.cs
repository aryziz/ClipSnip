using ClipSnip.Models;
using Microsoft.AspNetCore.Identity;

namespace ClipSnip.Data;

public static class ApplicationDbInitializer
{
    public static async Task Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {

        if (!await rm.RoleExistsAsync("User"))
        {
            await rm.CreateAsync(new IdentityRole("User"));
        }

        // Regular user
        var user = await um.FindByNameAsync("user");

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = "user",
                Email = "user@example.com",
                EmailConfirmed = true
            };

            var result =
                await um.CreateAsync(user, "Password1.");

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description)));
            }
        }

        if (!await um.IsInRoleAsync(user, "User"))
        {
            await um.AddToRoleAsync(user, "User");
        }

        await db.SaveChangesAsync();
    }
}