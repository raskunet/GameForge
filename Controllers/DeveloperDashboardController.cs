using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;

namespace GameForge.Controllers
{
    [Authorize(Roles = "Developer")]
        
    public class DeveloperDashboardController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public DeveloperDashboardController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<bool> TESTDEVAsync()
        {
        var user = await _userManager.GetUserAsync(User);
            
            bool Role = User.IsInRole("Developer");
            
            
            return Role;
        
        }
        private async Task<string> GetCurrentDeveloperIdAsync()
        {


            var user = await _userManager.GetUserAsync(User);
            
            return user.Id;
            // if (user is Developer developer)
            //     return developer.Id;

            // return null;
        }


        
        // GET: DeveloperDashboard
        public async Task<IActionResult> Index()
        {
            var role= await TESTDEVAsync();
            if (role== false) return Unauthorized();
            
            var developerId = await GetCurrentDeveloperIdAsync();
            Console.WriteLine("ye dev id ha bhai {0}",developerId);
            //if (developerId == null) return Unauthorized();
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

       

        // GET: DeveloperDashboard/Create
        public async Task<IActionResult> Create()
        {
            var developerId = await GetCurrentDeveloperIdAsync(); 
            ViewData["DeveloperId"] = developerId; 
            return View();
        }

        // POST: DeveloperDashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeveloperId,Title,Price,Description,Category,ImageUrl,ReleaseDate,DiscountPercentage,IsPaid")] Game game)
        {
            var developerId = await GetCurrentDeveloperIdAsync();
            if (ModelState.IsValid)
            {
                
                game.DeveloperId = developerId;
                game.ReleaseDate=DateTime.UtcNow;
                // var cgame=new Game({
                //     Title=Igame.Title,
                //     Price=Igame.Price,
                //     Description=Igame.Description,
                //     ReleaseDate=DateTime.UtcNow,
                //     DiscountPercentage=Igame.DiscountPercentage,
                //     IsPaid=Igame.IsPaid,
                //     ImageUrl=Igame.ImageUrl
                // });

                _context.Game.Add(game);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else 
            {
                var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new { ms.Key, ms.Value.Errors })
                .ToList();

                return BadRequest(errors);
                //Console.WriteLine("ye galat ha ");
            }

            //return View(game);
        }

        // GET: Edit Game (returns the edit view)
        public async Task<IActionResult> Edit(int id)
        {
            var developerId = await GetCurrentDeveloperIdAsync();

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
            var developerId = await GetCurrentDeveloperIdAsync();

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
        public async Task<IActionResult> ViewSales()
        {
            var developerId = await GetCurrentDeveloperIdAsync();

            // Fetch data first into memory and then perform operations
            var purchases = await _context.Purchase
                .Where(p => p.Game.DeveloperId == developerId)
                .Include(p => p.Game) // Ensure related data is loaded
                .ToListAsync(); // Fetch all relevant rows into memory

            // Calculate sales by month (total revenue)
            var salesByMonth = purchases
                .GroupBy(p => p.PurchaseDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(p => (double)p.PricePaid) // Convert decimal to double
                })
                .OrderBy(x => x.Month)
                .ToList();

            // Calculate the most sold games (total revenue per game)
            var mostSoldGames = purchases
                .GroupBy(p => p.Game.Title)
                .Select(g => new
                {
                    GameTitle = g.Key,
                    TotalRevenue = g.Sum(p => (double)p.PricePaid) // Convert decimal to double
                })
                .OrderByDescending(x => x.TotalRevenue)
                .Take(5)
                .ToList();

            // Calculate total revenue for lifetime
            var totalRevenue = purchases.Sum(p => (double)p.PricePaid); // Convert decimal to double

            ViewBag.TotalRevenue = totalRevenue; // Pass total revenue to the view

            // Prepare the view model
            var viewModel = new List<SalesDataViewModel>
            {
                new SalesDataViewModel
                {
                    Labels = salesByMonth
                        .Select(x => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Month))
                        .ToList(),
                    Data = salesByMonth.Select(x => x.TotalRevenue).ToList(),
                    ChartTitle = "Your Revenue by Month",
                    ChartType = "bar"
                },
                new SalesDataViewModel
                {
                    Labels = mostSoldGames.Select(x => x.GameTitle).ToList(),
                    Data = mostSoldGames.Select(x => x.TotalRevenue).ToList(),
                    ChartTitle = "Top Revenue Generating Games",
                    ChartType = "bar"
                }
            };

            return View(viewModel);
        }
         [HttpPost]
        public async Task<IActionResult> AddToFeatured(int id)
        {
            var userId = await GetCurrentDeveloperIdAsync();
            if (userId == null) return Unauthorized();

            var game = await _context.Game.FindAsync(id);
            if (game == null) return NotFound();

            // Check if the game is already in the user's cart
            var existingCartItem = _context.FeaturedGames.FirstOrDefault(c => c.GameID == id && c.UserID == userId);
            if (existingCartItem != null)
            {
                TempData["Message"] = "This game is already Featured";
                return RedirectToAction("Index","Featured");
            }
        
            // Add the game to the cart
            var featuredItem = new Featured
            {
                UserID = userId,
                GameID = id,
                FeaturingStartDate = DateTime.UtcNow // Add this field to the Cart model if not already present
            };

            _context.FeaturedGames.Add(featuredItem);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Game added to Featured!";
            return RedirectToAction("Index","Featured");
        }


        // [HttpPost]
        // public async Task<IActionResult> AddTestPurchase()
        // {
        //     
        //     var game = await _context.Game.FindAsync(1);
        //     var user = await _context.Users.FindAsync(2);

        //     if (game == null || user == null)
        //     {
        //         TempData["ErrorMessage"] = "Invalid GameId or UserId. Purchase could not be added.";
        //         return RedirectToAction(nameof(Index));
        //     }

        //     // Add the purchase
        //     var testPurchase = new Purchase
        //     {
        //         GameId = game.Id,
        //         UserId = user.Id,
        //         PurchaseDate = DateTime.Parse("2023-03-20"),
        //         PricePaid = 40
        //     };

        //     _context.Purchase.Add(testPurchase);
        //     await _context.SaveChangesAsync();

        //     TempData["Message"] = "Test purchase added successfully!";
        //     return RedirectToAction(nameof(Index));
        // }



        


        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
