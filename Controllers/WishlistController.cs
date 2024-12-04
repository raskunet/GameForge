using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GameForge.Controllers
{
    public class WishlistController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public WishlistController(GameForgeContext context, UserManager<User> userManager)
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

            var userGames = await _context.Wishlist
                .Include(l => l.game) // Include related game details
                .Where(l => l.UserID == userId)
                .ToListAsync();

            if (userGames == null || !userGames.Any())
            {
                return View("Empty");
            }

            var viewModel = new WishlistViewModel
            {
                Wishlist = userGames
            };

            return View(viewModel);
        }
        // Remove Game from Cart
        [HttpPost]
        public IActionResult RemoveGame(int wishlistId)
        {
            var cart = _context.Wishlist
            .FirstOrDefault(c => c.WishlistID == wishlistId);  // Only CartID

            if (cart != null)
            {
                _context.Wishlist.Remove(cart);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
