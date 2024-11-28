using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using GameForge.Data;
using System.Linq;

public class CartController : Controller
{
    private readonly GameForgeContext _context;

    public CartController(GameForgeContext context)
    {
        _context = context;
    }

    // Get Cart
    public IActionResult Index()
    {
        var userId = 1; // Replace with proper user identification method
        var cart = _context.Cart.Include(c => c.game).FirstOrDefault(c => c.UserID == userId && !c.IsCheckedOut);

        if (cart == null)
        {
            return View("EmptyCart");
        }

        // var cartItems = _context.Cart.Where(c => c.UserID == userId && !c.IsCheckedOut).ToList();
        var cartItems = _context.Cart
        .Include(c => c.game)  // Ensure Game is loaded
        .Where(c => c.UserID == userId && !c.IsCheckedOut)
        .ToList();

        // var totalPrice = cartItems.Sum(item => item.game.Price);
        var totalPrice = cartItems.Sum(item => item.game != null ? item.game.Price : 0);

        // Get TotalCollectables from the Collectables table for this user
        var collectables = _context.Collectables
        .FirstOrDefault(c => c.UserID == userId);

        var totalCollectables = collectables?.TotalCollectables ?? 0;

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

    // // Checkout Cart
    // [HttpPost]
    // public IActionResult Checkout()
    // {
    //     var userId = 1; // Replace with proper user identification method
    //     var cart = _context.Cart.Where(c => c.UserID == userId && !c.IsCheckedOut).ToList();

    //     foreach (var item in cart)
    //     {
    //         item.IsCheckedOut = true;  // Mark cart as checked out
    //     }

    //     _context.SaveChanges();
    //     return View("CheckoutConfirmation");
    // }
    [HttpPost]
    public IActionResult Checkout()
    {
        var userId = 1; // Replace with actual user identification method
        var cartItems = _context.Cart
            .Include(c => c.game) // Load related Game entity
            .Where(c => c.UserID == userId && !c.IsCheckedOut)
            .ToList();

        if (!cartItems.Any())
        {
            return RedirectToAction("EmptyCart");
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

        // Mark the cart as checked out
        foreach (var item in cartItems)
        {
            item.IsCheckedOut = true;
        }

        _context.SaveChanges();

        // Redirect to the checkout confirmation view
        var totalPrice = cartItems.Sum(item => item.game.Price);
        var viewModel = new CartViewModel
        {
            CartItems = cartItems,
            TotalPrice = totalPrice
        };

        return View("CheckoutConfirmation", viewModel);
    }

    public int GetTotalCollectables(int userId=1)
    {
        // Count the number of games in the user's library
        return _context.Libraries.Count(l => l.UserID == userId);
    }

}
