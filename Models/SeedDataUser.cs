using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameForge.Data;
using System;
using System.Linq;

namespace GameForge.Models
{
    public static class SeedDataUser
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Look for any Users
            using (var context = new GameForgeContext(
                serviceProvider.GetRequiredService<DbContextOptions<GameForgeContext>>()))
            {
                // Check if the database has been seeded
                if (context.User.Any())
                {
                    // Clear previous data if you want to reseed the database
                    // context.Game.RemoveRange(context.Game);
                    // context.Purchase.RemoveRange(context.Purchase);
                    // context.Review.RemoveRange(context.Review);
                    // context.User.RemoveRange(context.User);
                    // context.SaveChanges();
                    return;
                }


                var playerOne = new User
                {
                    Id = 1,
                    Username = "PlayerOne",
                    Email = "playerone@example.com",
                    PasswordHash = "hashedpassword3",
                    CreationDate=DateTime.UtcNow
                };

                var playerTwo = new User
                {
                    Id = 2,
                    Username = "PlayerTwo",
                    Email = "playertwo@example.com",
                    PasswordHash = "hashedpassword4",
                    CreationDate=DateTime.UtcNow
                };
                // Seed Developers
                var developers = new[]
                {
                    new Developer
                    {
                        Id = 3,
                        Username = "DevOne",
                        Email = "devone@example.com",
                        PasswordHash = "hashedpassword1",
                        CreationDate=DateTime.UtcNow
                    },
                    new Developer
                    {
                        Id = 4,
                        Username = "DevTwo",
                        Email = "devtwo@example.com",
                        PasswordHash = "hashedpassword2",
                        CreationDate=DateTime.UtcNow
                    }
                };

                context.User.AddRange(developers);
                context.SaveChanges();

                var devOne = context.User.OfType<Developer>().First(d => d.Username == "DevOne");
                var devTwo = context.User.OfType<Developer>().First(d => d.Username == "DevTwo");

                // Seed Games with Developer IDs
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

                

                context.User.AddRange(playerOne, playerTwo);
                context.SaveChanges();

                var purchases = new[]
                {
                    new Purchase { GameId = games[0].Id, UserId = playerOne.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[1].Id, UserId = playerOne.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[0].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[1].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[2].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[3].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow },
                    new Purchase { GameId = games[4].Id, UserId = playerTwo.Id, PurchaseDate = DateTime.UtcNow }
                };

                context.Purchase.AddRange(purchases);
                context.SaveChanges();

                // Seed Reviews (PlayerTwo has reviewed all games)
                var reviews = new[]
                {
                    new Review { GameId = games[1].Id, UserId = playerOne.Id, Rating = 1, Comment = "Pretty farig game!", CreatedAt = DateTime.UtcNow },
                    new Review { GameId = games[0].Id, UserId = playerTwo.Id, Rating = 4, Comment = "Pretty good game!", CreatedAt = DateTime.UtcNow },
                    new Review { GameId = games[1].Id, UserId = playerTwo.Id, Rating = 5, Comment = "Fantastic gameplay!", CreatedAt = DateTime.UtcNow },
                    new Review { GameId = games[2].Id, UserId = playerTwo.Id, Rating = 3, Comment = "Good but could be better.", CreatedAt = DateTime.UtcNow },
                    new Review { GameId = games[3].Id, UserId = playerTwo.Id, Rating = 4, Comment = "Very creative!", CreatedAt = DateTime.UtcNow },
                    new Review { GameId = games[4].Id, UserId = playerTwo.Id, Rating = 5, Comment = "The best racing game out there!", CreatedAt = DateTime.UtcNow }
                };

                context.Review.AddRange(reviews);
                context.SaveChanges();

                var libraris = new[]
                {
                    new Library { GameId = games[1].Id, UserID = playerOne.Id,LibraryCreationDate = DateTime.UtcNow },
                    new Library { GameId = games[0].Id, UserID = playerOne.Id,LibraryCreationDate = DateTime.UtcNow },
                    new Library { GameId = games[2].Id, UserID = playerOne.Id,LibraryCreationDate = DateTime.UtcNow }
                };

                context.Libraries.AddRange(libraris);
                context.SaveChanges();

                var Carts = new[]
                {
                    new Cart { GameId = games[4].Id, UserID = playerOne.Id,CreationDate=DateTime.UtcNow,IsCheckedOut=false },
                    new Cart { GameId = games[0].Id, UserID = playerOne.Id,CreationDate=DateTime.UtcNow,IsCheckedOut=false }
                };

                context.Cart.AddRange(Carts);
                context.SaveChanges();

                var Collectablis = new[]
                {
                    new Collectables {  UserID = playerOne.Id,TotalCollectables=300}
                };

                context.Collectables.AddRange(Collectablis);
                context.SaveChanges();
                


            }
        }
    }
}
