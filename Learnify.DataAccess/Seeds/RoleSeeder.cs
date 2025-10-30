using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Seeds
{
    public static class RoleSeed
    {
        public static async Task EnsureRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            var roles = new[] { "Admin", "Instructor", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }
    }
}
