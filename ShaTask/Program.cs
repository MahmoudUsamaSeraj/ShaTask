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
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ICasherRepo , CasherRepo>();
            builder.Services.AddScoped<IBranchRepo, BranchRepo>();
            builder.Services.AddScoped<ICityRepo, CityRepo>();

            builder.Services.AddDbContext<ShaTaskContext>(options=>options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ShaTaskCon")));
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ShaTaskContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

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
