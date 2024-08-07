using Microsoft.AspNetCore.Identity;
using ShaTask.Needs;

namespace ShaTask.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Adminstrator.ToString()));
        }
    }
}
