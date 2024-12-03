using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameForge.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;
        public LibraryController(GameForgeContext context, UserManager<User> userManager)
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
            if (userId == null) return Unauthorized();

            var userGames = await _context.Libraries
                .Include(l => l.game ) // Include related game details
                .Where(l => l.UserID == userId)
                .ToListAsync();

            if (userGames == null || !userGames.Any())
            {
                return View("Empty");
            }
            var collectables = _context.Collectables
            .FirstOrDefault(c => c.UserID == userId);
            var returnGames = new LibraryViewModel
            {
                Items=userGames,
                TotalCollectables=collectables.TotalCollectables
            }; 
            return View(returnGames);
        }
    }
}
