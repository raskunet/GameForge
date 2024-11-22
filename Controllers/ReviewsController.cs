using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GameForge.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly GameForgeContext _context;

        public ReviewsController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: Reviews/Create
        public IActionResult Create(int gameId)
        {

            
            // Assuming user is hardcoded for now
            var userId = 3; // Replace with actual user logic later

            // Check if the user has purchased the game
            bool hasPurchased = _context.Purchase
                .Any(p => p.GameId == gameId && p.UserId == userId);

            if (!hasPurchased)
            {
                return Unauthorized("You can only review games that you have purchased.");
            }
            var review = new Review { GameId = gameId, UserId = userId };
            return View(review);
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Rating,Comment")] Review review)
        {
            // Assuming user is hardcoded for now
            var userId = 3; // Replace with actual user logic later
            review.UserId = userId;

            // Check if the user has purchased the game
            bool hasPurchased = _context.Purchase
                .Any(p => p.GameId == review.GameId && p.UserId == userId);

            if (!hasPurchased)
            {
                return Unauthorized("You can only review games that you have purchased.");
            }

            if (ModelState.IsValid)
            {
                review.CreatedAt = DateTime.UtcNow;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = 3; // Replace with actual user logic later

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            // Check if the review belongs to the current user
            if (review.UserId != userId)
            {
                return Unauthorized("You can only edit your own reviews.");
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,Rating,Comment")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            // Assuming user is hardcoded for now
            var userId = 3; // Replace with actual user logic later

            // Ensure the review belongs to the current user
            var existingReview = await _context.Review.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (existingReview == null)
            {
                return Unauthorized("You can only edit your own reviews.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.UserId = userId; // Ensure the review is updated by the correct user
                    review.CreatedAt = existingReview.CreatedAt; // Preserve original creation date
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Assuming user is hardcoded for now
            var userId = 3; // Replace with actual user logic later

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            // Ensure the review belongs to the current user
            if (review.UserId != userId)
            {
                return Unauthorized("You can only delete your own reviews.");
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Games", new { id = review.GameId });
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
