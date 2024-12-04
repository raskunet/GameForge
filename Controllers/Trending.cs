using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameForge.Controllers
{
    public class TrendingController : Controller
    {
        private readonly GameForgeContext _context;
        
        private readonly UserManager<User> _userManager;

        public TrendingController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // GET: Library/UserGames/5
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null)
            {
                return BadRequest("Invalid user ID.");
            }

            // var userGames = (await _context.Game // Include related game details
            //     .Include(l => l.Reviews)
            //     .ToListAsync())
            //     .Where(l => l.AverageRating > 3.9);
            var userGames = (await _context.Game
            .Include(l => l.Reviews)
            .ToListAsync()) // Include related reviews
            .Where(l => l.AverageRating >= 0) // Optional: Filter out games with no rating or invalid data
            .OrderByDescending(l => l.AverageRating) // Sort by rating in descending order
            .Take(3); // Take the top 3 games


            if (userGames == null || !userGames.Any())
            {
                return NotFound($"No games is Trending");
            }

            return View(userGames);
        }
    }
}
