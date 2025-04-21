using Microsoft.AspNetCore.Identity;
using ExpertBooking.Core.Entities;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleSeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRolesAndAdminAsync()
    {
        string[] roleNames = { "Admin", "Expert", "Client" };

        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
            }
        }

        var adminEmail = "developer.abdullah92@gmail.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Abdullah",
                LastName = "Hemida",
                IsProfileCompleted = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newAdmin, "Admin@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}

