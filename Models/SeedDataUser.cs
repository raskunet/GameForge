using GameForge.Data;
using GameForge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameForge.Models;

public static class SeedDataUser
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GameForgeContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<GameForgeContext>>()))
        {
            // Look for any Users
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }
            context.User.AddRange(
                new User
                {
                    Username="Username 1",
                    CreationDate=DateTime.UtcNow,
                    Password="123456789",
                    Email="username1@gmail.com"
                },
                new User
                {
                    Username="Username 2",
                    CreationDate=DateTime.UtcNow,
                    Password="123456789",
                    Email="username2@gmail.com"
                },
                new User
                {
                    Username="Username 3",
                    CreationDate=DateTime.UtcNow,
                    Password="123456789",
                    Email="username3gmail.com"
                },
                new User
                {
                    Username="Username 4",
                    CreationDate=DateTime.UtcNow,
                    Password="123456789",
                    Email="username4@gmail.com"
                }
            );
            context.SaveChanges();
        }
    }
}