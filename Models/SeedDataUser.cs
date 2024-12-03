using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameForge.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using GameForge.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GameForgeContext(
            serviceProvider.GetRequiredService<DbContextOptions<GameForgeContext>>()))
        {
            // Ensure database is clean if reseeding
            if (context.Game.Any())
            {
                // context.Game.RemoveRange(context.Game);
                // context.Purchase.RemoveRange(context.Purchase);
                // context.Review.RemoveRange(context.Review);
                // context.Users.RemoveRange(context.Users);
                // context.SaveChanges();
                return;
            }

            // Create roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Developer", "User" };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            // Seed Users
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var developers = new[]
            {
                new User
                {
                    UserName = "DevOne",
                    Email = "devone@example.com",
                    IsDeveloper = true
                },
                new User
                {
                    UserName = "DevTwo",
                    Email = "devtwo@example.com",
                    IsDeveloper = true
                }
            };

            var players = new[]
            {
                new User
                {
                    UserName = "PlayerOne",
                    Email = "playerone@example.com",
                    IsDeveloper = false
                },
                new User
                {
                    UserName = "PlayerTwo",
                    Email = "playertwo@example.com",
                    IsDeveloper = false
                }
            };

            // Add developers and users
            foreach (var dev in developers)
            {
                var result = userManager.CreateAsync(dev, "Password123!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(dev, "Developer").Wait();
                }
            }

            foreach (var player in players)
            {
                var result = userManager.CreateAsync(player, "Password123!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(player, "User").Wait();
                }
            }

            context.SaveChanges();

            // Fetch seeded users by role
            var devOne = userManager.FindByNameAsync("DevOne").Result;
            var devTwo = userManager.FindByNameAsync("DevTwo").Result;
            var playerOne = userManager.FindByNameAsync("PlayerOne").Result;
            var playerTwo = userManager.FindByNameAsync("PlayerTwo").Result;

            var games = new[]
                {
                    new Game
                    {
                        Title = "The Last Explorer",
                        Description = "An open-world adventure where you explore unknown lands.",
                        ImageUrl = "/images/last_explorer.jpg",
                        ReleaseDate = DateTime.UtcNow,
                        Price = 49.99M,
                        Category = "Adventure",
                        GameplayLink = "https://www.youtube.com/watch?v=explorer_gameplay",
                        DeveloperId = devOne.Id
                    },
                    new Game
                    {
                        Title = "Space Wars: Galaxy Battles",
                        Description = "A futuristic space combat game with stunning visuals.",
                        ImageUrl = "/images/space_wars.jpg",
                        ReleaseDate = DateTime.UtcNow,
                        Price = 59.99M,
                        Category = "Action",
                        GameplayLink = "https://www.youtube.com/watch?v=space_wars_gameplay",
                        DeveloperId = devOne.Id
                    },
                    new Game
                    {
                        Title = "Mystic Legends",
                        Description = "An RPG game set in a mythical world filled with magic.",
                        ImageUrl = "/images/mystic_legends.jpg",
                        ReleaseDate = DateTime.UtcNow,
                        Price = 39.99M,
                        Category = "RPG",
                        GameplayLink = "https://www.youtube.com/watch?v=mystic_legends_gameplay",
                        DeveloperId = devOne.Id
                    },
                    new Game
                    {
                        Title = "City Architect",
                        Description = "A simulation game where you design and manage a futuristic city.",
                        ImageUrl = "/images/city_architect.jpg",
                        ReleaseDate = DateTime.UtcNow,
                        Price = 29.99M,
                        Category = "Simulation",
                        GameplayLink = "https://www.youtube.com/watch?v=city_architect_gameplay",
                        DeveloperId = devTwo.Id
                    },
                    new Game
                    {
                        Title = "Racing Kings",
                        Description = "An exhilarating racing game with realistic car physics.",
                        ImageUrl = "/images/racing_kings.jpg",
                        ReleaseDate = DateTime.UtcNow,
                        Price = 19.99M,
                        Category = "Racing",
                        GameplayLink = "https://www.youtube.com/watch?v=racing_kings_gameplay",
                        DeveloperId = devTwo.Id
                    }
                };
            context.Game.AddRange(games);
            context.SaveChanges();

            // Seed Purchases
            var purchases = new[]
            {
                new Purchase { GameId = games[0].Id, UserId = playerOne.Id, PurchaseDate = DateTime.UtcNow, PricePaid = 20 },
                new Purchase { GameId = games[1].Id, UserId = playerOne.Id, PurchaseDate = DateTime.UtcNow, PricePaid = 30 },
                new Purchase { GameId = games[3].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow, PricePaid = 40 },
                new Purchase { GameId = games[4].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow, PricePaid = 50 }
            };

            context.Purchase.AddRange(purchases);
            context.SaveChanges();

            // Seed Reviews
            var reviews = new[]
            {
                new Review { GameId = games[0].Id, UserId = playerTwo.Id, Rating = 4, Comment = "Good game!" },
                new Review { GameId = games[1].Id, UserId = playerOne.Id, Rating = 5, Comment = "Fantastic gameplay!" },
                new Review { GameId = games[2].Id, UserId = playerTwo.Id, Rating = 3, Comment = "Could be better." },
                new Review { GameId = games[3].Id, UserId = playerTwo.Id, Rating = 4, Comment = "Very creative!" }
            };

            context.Review.AddRange(reviews);
            context.SaveChanges();
        }
    }
}