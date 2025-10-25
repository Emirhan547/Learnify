using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Learnify.UI.Seeds
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager, UserManager<AppUser> userManager)
        {
            // Rolleri oluştur
            string[] roles = { "Admin", "Instructor", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // Admin kullanıcı oluştur (eğer yoksa)
            var adminEmail = "admin@learnify.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "Admin User",
                    EmailConfirmed = true,
                    Profession = "Administrator"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Instructor kullanıcı oluştur (test için)
            var instructorEmail = "instructor@learnify.com";
            var instructorUser = await userManager.FindByEmailAsync(instructorEmail);

            if (instructorUser == null)
            {
                instructorUser = new AppUser
                {
                    UserName = "instructor",
                    Email = instructorEmail,
                    FullName = "Test Instructor",
                    EmailConfirmed = true,
                    Profession = "Web Developer"
                };

                var result = await userManager.CreateAsync(instructorUser, "Instructor123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(instructorUser, "Instructor");
                }
            }

            // Student kullanıcı oluştur (test için)
            var studentEmail = "student@learnify.com";
            var studentUser = await userManager.FindByEmailAsync(studentEmail);

            if (studentUser == null)
            {
                studentUser = new AppUser
                {
                    UserName = "student",
                    Email = studentEmail,
                    FullName = "Test Student",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(studentUser, "Student123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");
                }
            }
        }
    }
}