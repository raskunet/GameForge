using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameForge.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameForgeContext _context;

        public GamesController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index(string gameCategory, string searchString)
        {
            if (_context.Game == null)
            {
                return Problem("Entity set 'GameForgeContext.Game' is null.");
            }

            // Use LINQ to get list of categories.
            IQueryable<string> categoryQuery = from g in _context.Game
                                            orderby g.Category
                                            select g.Category;

            var games = from g in _context.Game
                        select g;

            // Filtering by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            // Filtering by selected category
            if (!string.IsNullOrEmpty(gameCategory))
            {
                games = games.Where(x => x.Category == gameCategory);
            }

            // Creating the ViewModel for search/filter
            var gameCategoryVM = new GameCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Games = await games.ToListAsync()
            };

            return View(gameCategoryVM);
        }

        // GET: Games/Details/5
       public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Reviews)  // Include the reviews for this game
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }
            bool hasPurchased = _context.Purchase.Any(p => p.GameId == id && p.UserId == 1);
            ViewData["PurchaseCond"] = hasPurchased;
            return View(game);
        }
        // This method checks if the game exists in the database
        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
