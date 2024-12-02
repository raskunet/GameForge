using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;

namespace GameForge.Controllers
{
    public class TrendingController : Controller
    {
        private readonly GameForgeContext _context;

        public TrendingController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: Library/UserGames/5
        public async Task<IActionResult> Index(int userId=1)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var userGames = (await _context.Game // Include related game details
                .Include(l => l.Reviews)
                .ToListAsync())
                .Where(l => l.AverageRating > 3.9);

            if (userGames == null || !userGames.Any())
            {
                return NotFound($"No games found for user ID: {userId}");
            }

            return View(userGames);
        }
    }
}
