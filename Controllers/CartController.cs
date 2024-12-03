using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

public class CartController : Controller
{
    private readonly GameForgeContext _context;
    
    private readonly UserManager<User> _userManager;

    public CartController(GameForgeContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    private async Task<string> GetCurrentUserIdAsync()
    {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
    }    

    // Get Cart
    public async Task<IActionResult> Index()
    {
        // var userId = 1; // Replace with proper user identification method
        var userId = await GetCurrentUserIdAsync();
        if (userId == null) return Unauthorized();
        if(!_context.Cart.Any(c => c.UserID == userId && !c.IsCheckedOut))
        {
            return View("EmptyCart");
        }
        var cart = _context.Cart.Include(c => c.game).FirstOrDefault(c => c.UserID == userId && !c.IsCheckedOut);

        // var cartItems = _context.Cart.Where(c => c.UserID == userId && !c.IsCheckedOut).ToList();
        var cartItems = _context.Cart
        .Include(c => c.game)  // Ensure Game is loaded
        .Where(c => c.UserID == userId && !c.IsCheckedOut)
        .ToList();

        // var totalPrice = cartItems.Sum(item => item.game.Price);
        var totalPrice = cartItems.Sum(item => item.game != null ? item.game.Price : 0);

        // Get TotalCollectables from the Collectables table for this user
        var collectables = _context.Wallets
        .FirstOrDefault(c => c.UserID == userId);

        var totalCollectables = collectables?.TotalAmount ?? 1000;

        if (cart == null)
        {
            return View("EmptyCart");
        }

        var viewModel = new CartViewModel
        {
            CartItems = cartItems,
            TotalPrice = totalPrice,
            TotalCollectables = totalCollectables
        };

        return View(viewModel);
    }

    // Remove Game from Cart
    [HttpPost]
    public IActionResult RemoveGame(int cartId)
    {
        var cart = _context.Cart
        .FirstOrDefault(c => c.CartID == cartId);  // Only CartID

        if (cart != null)
        {
            _context.Cart.Remove(cart);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
         var userId = await GetCurrentUserIdAsync();
        if (userId == null) return Unauthorized(); // Replace with actual user identification method
        var cartItems = _context.Cart
            .Include(c => c.game) // Load related Game entity
            .Where(c => c.UserID == userId && !c.IsCheckedOut)
            .ToList();

        if (!cartItems.Any())
        {
            return RedirectToAction("EmptyCart");
        }
        // Redirect to the checkout confirmation view
        var totalPrice = cartItems.Sum(item => item.game.PriceAfterDiscount);
        // Get TotalCollectables from the Collectables table for this user
        var collectables = _context.Wallets
        .FirstOrDefault(c => c.UserID == userId);

        var totalCollectables = collectables?.TotalAmount ?? 1000;
        if (totalPrice > totalCollectables)
        {
            return BadRequest("Not enough creds");
        }
         // Deduct the total price from the user's collectables
        if (collectables != null)
        {
            collectables.TotalAmount -= totalPrice;
        }
        // Add games to the user's library
        foreach (var item in cartItems)
        {
            var libraryEntry = new Library
            {
                UserID = userId, // Set the user's ID
                GameId = item.GameId, // Associate the game
                LibraryCreationDate = DateTime.UtcNow
            };

            _context.Libraries.Add(libraryEntry);
        }
        decimal totalC=0;
        // Mark the cart as checked out
        foreach (var item in cartItems)
        {
            item.IsCheckedOut = true;
            totalC+=100;
        }

        // _context.SaveChanges();

        foreach (var item in cartItems)
        {
            // Check if the user already purchased the game
            var existingPurchase = _context.Purchase.FirstOrDefault(p => p.GameId == item.GameId && p.UserId == userId);
            if (existingPurchase == null)
            {
                var purchase = new Purchase
                {
                    UserId = userId,
                    GameId = item.GameId,
                    PurchaseDate = DateTime.UtcNow,
                    PricePaid = item.game.PriceAfterDiscount
                };
                _context.Purchase.Add(purchase);
            }
                
        }
        var existingCollectableUser = _context.Collectables.FirstOrDefault(p => p.UserID == userId);
        if(existingCollectableUser == null)
        {
            var New = new Collectables
            {
                UserID = userId,
                TotalCollectables=0
            };
            _context.Collectables.Add(New);
        }
        _context.SaveChanges();
        
        var collectable = _context.Collectables.FirstOrDefault(c => c.UserID == userId);
        collectable.TotalCollectables+=totalC;

        _context.SaveChanges();
        var viewModel = new CartViewModel
        {
            CartItems = cartItems,
            TotalPrice = totalPrice
        };

        return View("CheckoutConfirmation", viewModel);
    }


}
