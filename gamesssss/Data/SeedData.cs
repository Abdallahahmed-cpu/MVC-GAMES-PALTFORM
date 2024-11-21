using Microsoft.AspNetCore.Identity;

namespace gamesssss.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin@gamesssss.com",
                Email = "admin@gamesssss.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };

            var adminPassword = "Admin@123";
            var existingUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (existingUser == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }

}
