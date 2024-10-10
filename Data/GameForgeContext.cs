using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;

namespace GameForge.Data
{
    public class GameForgeContext : DbContext
    {
        public GameForgeContext (DbContextOptions<GameForgeContext> options)
            : base(options)
        {
        }

        public DbSet<GameForge.Models.User> User { get; set; } = default!;
    }
}
