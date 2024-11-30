using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GameForge.Controllers
{
    public class DeveloperDashboardController : Controller
    {
        private readonly GameForgeContext _context;

        public DeveloperDashboardController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: DeveloperDashboard
        public async Task<IActionResult> Index()
        {
            // Hardcoded developer ID for now; replace with actual logic
            int developerId = 3;
            
            var games = await _context.Game
                .Where(g => g.DeveloperId == developerId)
                .ToListAsync();

            return View(games);
        }

        // POST: Apply Discount
        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(int gameId, int discountPercentage)
        {
            var game = await _context.Game.FindAsync(gameId);
            if (game == null) return NotFound();

            // Apply discount by updating only the discount percentage
            game.DiscountPercentage = discountPercentage;

            _context.Update(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Toggle Paid Status
        [HttpPost]
        public async Task<IActionResult> TogglePaidStatus(int gameId)
        {
            var game = await _context.Game.FindAsync(gameId);
            if (game == null) return NotFound();

            // Toggle paid status
            game.IsPaid = !game.IsPaid;

            _context.Update(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit Game (returns the edit view)
        // Assuming there's a way to get the current developer's ID
        int GetCurrentDeveloperId() 
        {
            // Replace this with actual logic, e.g., retrieving the developer ID from the logged-in user
            return 3; // Example ID; in production, this should be dynamic
        }

        // GET: Edit Game (returns the edit view)
        public async Task<IActionResult> Edit(int id)
        {
            var developerId = GetCurrentDeveloperId(); // Retrieve current developer ID

            // Retrieve game and check if it belongs to the current developer
            var game = await _context.Game
                .Where(g => g.Id == id && g.DeveloperId == developerId)
                .FirstOrDefaultAsync();

            if (game == null) 
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this game.";
                return RedirectToAction("Index"); // Redirect if unauthorized
            }

            return View(game);
        }

        // POST: Edit Game (saves the edited game)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,IsPaid,Description,Category,ImageUrl,ReleaseDate,DiscountPercentage,DeveloperId")] Game game)
        {
            var developerId = GetCurrentDeveloperId(); // Retrieve current developer ID

            // Check if game belongs to the developer
            if (id != game.Id || game.DeveloperId != developerId) 
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this game.";
                return RedirectToAction("Index"); // Redirect if unauthorized
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // POST: Delete Game
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null) return NotFound();

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }

        // [HttpGet, ActionName("Problem")]
        // public async Task<IActionResult> ViewProblems(int GameID)
        // {
        //     var gameList = await _context.GameProblems.FirstOrDefaultAsync(m => m.GameID == GameID);
        //     if (gameList == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(gameList);
        // }
    }
}
