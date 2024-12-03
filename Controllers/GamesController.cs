using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace GameForge.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public GamesController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
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

            var userId = await GetCurrentUserIdAsync();

            // Pass the logged-in user ID to the view
            ViewData["CurrentUserId"] = userId;

            // Check if the user has purchased the game
            bool hasPurchased = _context.Purchase.Any(p => p.GameId == id && p.UserId == userId);
            ViewData["PurchaseCond"] = hasPurchased;

            return View(game);
        }
        //[Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return Unauthorized();

            // Check if the game exists
            var game = await _context.Game.FindAsync(id);
            if (game == null) return NotFound();

            // Check if the game is already in the user's cart
            var existingCartItem = _context.Cart.FirstOrDefault(c => c.GameId == id && c.UserID == userId && !c.IsCheckedOut);
            if (existingCartItem != null)
            {
                TempData["Message"] = "This game is already in your cart.";
                return RedirectToAction("Index","Cart");
            }
            // Check if the game is already in the user's cart
            var existingLibItem = _context.Libraries.FirstOrDefault(c => c.GameId == id && c.UserID == userId);
            if (existingLibItem != null)
            {
                ViewData["WarningMessage"] = "You already Own this game";
                return RedirectToAction("Index","Games");
            }

            // Add the game to the cart
            var cartItem = new Cart
            {
                UserID = userId,
                GameId = id,
                IsCheckedOut = false,
                CreationDate = DateTime.UtcNow // Add this field to the Cart model if not already present
            };

            _context.Cart.Add(cartItem);
            await _context.SaveChangesAsync();


            
            // Check if the user already purchased the game
            var existingPurchase = _context.Purchase.FirstOrDefault(p => p.GameId == id && p.UserId == userId);
            if (existingPurchase != null)
            {
                var purchase = new Purchase
                {
                    UserId = userId,
                    GameId = id,
                    PurchaseDate = DateTime.UtcNow,
                    PricePaid = game.PriceAfterDiscount
                };
                TempData["Message"] = "You already own this game!";
                return RedirectToAction(nameof(Details), new { id });
            }

            // Create a new purchase
            var purchase = new Purchase
            {
                UserId = userId,
                GameId = id,
                PurchaseDate = DateTime.UtcNow,
        


                PricePaid = game.PriceAfterDiscount
            };

                _context.Purchase.Add(purchase);
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Game added to your cart!";
            return RedirectToAction("Index","Cart");
        }
         [HttpPost]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return Unauthorized();

            var game = await _context.Game.FindAsync(id);
            if (game == null) return NotFound();

            // Check if the game is already in the user's cart
            var existingCartItem = _context.Wishlist.FirstOrDefault(c => c.GameId == id && c.UserID == userId);
            if (existingCartItem != null)
            {
                TempData["Message"] = "This game is already in your Wishlist.";
                return RedirectToAction("Index","Wishlist");
            }
        
            // Add the game to the cart
            var wishlistItem = new Wishlist
            {
                UserID = userId,
                GameId = id,
                WishlistAdditionDate = DateTime.UtcNow // Add this field to the Cart model if not already present
            };

            _context.Wishlist.Add(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Game added to your Wishlist!";
            return RedirectToAction("Index","Wishlist");
        }


        //[Authorize(Roles = "User")]
        // [HttpPost]
        // public async Task<IActionResult> Purchase(int id)
        // {
        //     var userId = await GetCurrentUserIdAsync();
        //     if (userId == null) return Unauthorized();

        //     // Check if the game exists
        //     var game = await _context.Game.FindAsync(id);
        //     if (game == null) return NotFound();

        //     // Check if the user already purchased the game
        //     //var existingPurchase = _context.Purchase.FirstOrDefault(p => p.GameId == id && p.UserId == userId);
        //     // if (existingPurchase != null)
        //     // {
        //     //     TempData["Message"] = "You already own this game!";
        //     //     return RedirectToAction(nameof(Details), new { id });
        //     // }

        //     // Create a new purchase
        //     var purchase = new Purchase
        //     {
        //         UserId = userId,
        //         GameId = id,
        //         PurchaseDate = DateTime.UtcNow,
        //         PricePaid = game.PriceAfterDiscount
        //     };

        //     _context.Purchase.Add(purchase);
        //     await _context.SaveChangesAsync();

        //     TempData["Message"] = "Purchase successful!";
        //     return RedirectToAction("Index","Cart");
        // }



        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
