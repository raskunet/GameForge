using GameForge.Data;
using GameForge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameForge.Models;

public static class SeedDataTag
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GameForgeContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<GameForgeContext>>()))
        {
            // Look for any Tags
            if (context.ThreadTags.Any())
            {
                return;   // DB has been seeded
            }
            context.ThreadTags.AddRange(
                new ThreadTag{
                    TagName="Game Development",
                    CreationDate=DateTime.UtcNow
                },
                new ThreadTag{
                    TagName="Game Discussion",
                    CreationDate=DateTime.UtcNow
                },
                new ThreadTag{
                    TagName="Game Updates/Patches",
                    CreationDate=DateTime.UtcNow
                },
                new ThreadTag{
                    TagName="fAB Theories",
                    CreationDate=DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }
    }
}