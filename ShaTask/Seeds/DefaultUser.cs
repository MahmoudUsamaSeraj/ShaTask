using Microsoft.AspNetCore.Identity;
using ShaTask.Needs;
using ShaTask.Models;
using System.Data;
using System.Security.Claims;
using System.Reflection;

namespace ShaTask.Seeds
{
    public static class DefaultUser
    {
        public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
			var defaultUser = new Account
			{
				UserName = "admin@domain.com",
				Email = "admin@domain.com",
				EmailConfirmed = true,

			};

			var user = await userManager.FindByEmailAsync(defaultUser.Email);

			if (user == null)
			{
				await userManager.CreateAsync(defaultUser, "admin123");
			}

			await userManager.AddToRoleAsync(defaultUser, Roles.Adminstrator.ToString());
			await roleManager.SeedClaimsForSuperUser();

		}
		private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(Roles.Adminstrator.ToString());
            await roleManager.AddPermissionClaims(superAdminRole, Modules.City.ToString());
            await roleManager.AddPermissionClaims(superAdminRole, Modules.Cashier.ToString());
            await roleManager.AddPermissionClaims(superAdminRole, Modules.Branch.ToString());
            await roleManager.AddPermissionClaims(superAdminRole, Modules.InvoiceHeader.ToString());
            await roleManager.AddPermissionClaims(superAdminRole, Modules.InvoiceDetail.ToString());

            await roleManager.AddClaimAsync(superAdminRole, new Claim("Permission", $"Permissions.Cashiers.View"));

        }

        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permission.GeneratePermissionsList(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }

    }
}
