using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;

namespace GameForge.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameForgeContext _context;

        public LibraryController(GameForgeContext context)
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

            var userGames = await _context.Libraries
                .Include(l => l.game) // Include related game details
                .Where(l => l.UserID == userId)
                .ToListAsync();

            if (userGames == null || !userGames.Any())
            {
                return NotFound($"No games found for user ID: {userId}");
            }

            return View(userGames);
        }
    }
}
