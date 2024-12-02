using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameForge.Data;
using GameForge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GameForge
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            var root = Directory.GetCurrentDirectory();

            var dotenv = Path.Combine(root, ".env");
            if (!DotEnv.PGSQLConnStringLoad(dotenv, "POSTGRES"))
            {
                Environment.ExitCode = -1;
                return;
            }


            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GameForgeContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES")));


            //builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<GameForgeContext>();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<GameForgeContext>()
            .AddDefaultUI() // This ensures the default UI is included
            .AddDefaultTokenProviders();



            // builder.Services.AddAuthentication(options =>
            // {
            //     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // }).AddCookie(options =>
            // {
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //     options.Cookie.MaxAge = options.ExpireTimeSpan;
            // });



            // Add services to the container.
            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            // Seed roles before running the app
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedRoles(roleManager);  // Call the seed method
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedDataTag.Initialize(services);
            }
            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Default HSTS value is 30 days
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Ensure correct order of middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();

            // Static method to seed roles
            static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                var roles = new[] { "Developer", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }


        }
    }
}