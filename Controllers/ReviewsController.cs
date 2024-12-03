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
    public class ReviewsController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public ReviewsController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // GET:     Reviews/Create

        public async Task<IActionResult> Create(int gameId)
        {
            var userId = await GetCurrentUserIdAsync();
            ViewData["UId"] = userId;
            Console.WriteLine("ye user id ha bhai {0}", userId);
            // Check if the user has purchased the game
            bool hasPurchased = _context.Purchase
                .Any(p => p.GameId == gameId && p.UserId == userId);

            if (!hasPurchased)
            {
                return Unauthorized("You can only review games that you have purchased.");
            }
            // var review = new Review { GameId = gameId, UserId = userId };
            // return View(review);

            return View();
        }


        // POST: Reviews/Create
        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,GameId,Rating,Comment")] Review review)
        {
            var userId = await GetCurrentUserIdAsync();
            review.UserId = userId;

            // Verify purchase
            bool hasPurchased = _context.Purchase.Any(p => p.GameId == review.GameId && p.UserId == userId);
            if (!hasPurchased)
            {
                return Unauthorized("You can only review games you have purchased.");
            }

            bool alreadyReviewed = _context.Review.Any(r => r.GameId == review.GameId && r.UserId == userId);
            if (alreadyReviewed)
            {
                return BadRequest("You have already reviewed this game.");
            }

            
            if (ModelState.IsValid)
            {
                review.CreatedAt = DateTime.UtcNow;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }

            var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new { ms.Key, ms.Value.Errors })
                .ToList();

            return BadRequest(errors);
        }


        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var userId = await GetCurrentUserIdAsync();

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            if (review.UserId != userId)
            {
                return Unauthorized("You can only edit your own reviews.");
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GameId,Rating,Comment")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            // Assuming user is hardcoded for now
            var userId = await GetCurrentUserIdAsync();

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
            var userId = await GetCurrentUserIdAsync();

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
