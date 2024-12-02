using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameForge.Controllers
{
    public class FeaturedController : Controller
    {
        private readonly GameForgeContext _context;
        
        private readonly UserManager<User> _userManager;

        public FeaturedController(GameForgeContext context, UserManager<User> userManager)
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
            var userGames = await _context.FeaturedGames
            .Include(l => l.game)
            .ToListAsync();


            if (userGames == null || !userGames.Any())
            {
                return View("Empty");
            }

            return View(userGames);
        }
        // Remove Game from Cart
        [HttpPost]
        [Authorize(Roles = "Developer")]
        public IActionResult RemoveFeatured(int FeaturedId)
        {
            var cart = _context.FeaturedGames
            .FirstOrDefault(c => c.Id == FeaturedId);  // Only CartID

            if (cart != null)
            {
                _context.FeaturedGames.Remove(cart);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
