using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameForge.Data;
using GameForge.Models;
using GameForge.MiddleWare;

namespace GameForge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.PGSQLConnStringLoad(dotenv,"POSTGRES");


            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<GameForgeContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES")));
            
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedDataUser.Initialize(services);
                SeedDataTag.Initialize(services);
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.Map("/Question/Create", FormatTextMiddleWareExt.UseFormatText);
            // TODO : Make a custom MiddleWare Class for handling this 
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/Question/Create", StringComparison.OrdinalIgnoreCase), appBuild=>appBuild.UseMiddleware<FormatTextMiddleWare>());


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}