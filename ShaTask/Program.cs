using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShaTask.Models;
using ShaTask.Repository.BranchRepo;
using ShaTask.Repository.CasherRepo;
using ShaTask.Repository.CityRepo;

namespace ShaTask
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ICasherRepo , CasherRepo>();
            builder.Services.AddScoped<IBranchRepo, BranchRepo>();
            builder.Services.AddScoped<ICityRepo, CityRepo>();

            builder.Services.AddDbContext<ShaTaskContext>(options=>options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ShaTaskCon")).EnableSensitiveDataLogging());
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            })
        .AddEntityFrameworkStores<ShaTaskContext>().AddDefaultUI().AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            
                var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerProvider>();
            var logger = loggerFactory.CreateLogger("app");
            try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await Seeds.DefaultRoles.SeedAsync(roleManager);
                    await Seeds.DefaultUser.Initialize(userManager, roleManager);
                logger.LogInformation("Data seeded");
                logger.LogInformation("Application Started");
            }
                catch (Exception ex)
                {
                   
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
