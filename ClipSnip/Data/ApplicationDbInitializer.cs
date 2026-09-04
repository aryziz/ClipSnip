using ClipSnip.Models;
using Microsoft.AspNetCore.Identity;

namespace ClipSnip.Data;

public static class ApplicationDbInitializer
{
    private const string UserRole = "User";
    private const string SeedEmail = "user@example.com";
    private const string SeedPassword = "Password1.";

    public static async Task InitializeAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(UserRole))
        {
            var roleResult = await roleManager.CreateAsync(
                new IdentityRole(UserRole));

            EnsureSucceeded(roleResult, "creating the User role");
        }

        var user = await userManager.FindByEmailAsync(SeedEmail)
            ?? await userManager.FindByNameAsync("user");

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = SeedEmail,
                Email = SeedEmail,
                EmailConfirmed = true
            };

            var userResult = await userManager.CreateAsync(
                user,
                SeedPassword);

            EnsureSucceeded(userResult, "creating the seed user");
        }

        if (!string.Equals(user.UserName, SeedEmail, StringComparison.OrdinalIgnoreCase))
        {
            var usernameResult = await userManager.SetUserNameAsync(
                user,
                SeedEmail);

            EnsureSucceeded(usernameResult, "setting the seed user username");
        }

        if (!await userManager.IsInRoleAsync(user, UserRole))
        {
            var roleAssignmentResult = await userManager.AddToRoleAsync(
                user,
                UserRole);

            EnsureSucceeded(
                roleAssignmentResult,
                "assigning the User role");
        }

        if (user.EmailConfirmed == false)
        {
            user.EmailConfirmed = true;

            var updateResult = await userManager.UpdateAsync(user);

            EnsureSucceeded(updateResult, "confirming the seed user email");
        }
    }

    private static void EnsureSucceeded(
        IdentityResult result,
        string operation)
    {
        if (result.Succeeded)
        {
            return;
        }

        var errors = string.Join(
            "; ",
            result.Errors.Select(error =>
                $"{error.Code}: {error.Description}"));

        throw new InvalidOperationException(
            $"Identity failed while {operation}: {errors}");
    }
}