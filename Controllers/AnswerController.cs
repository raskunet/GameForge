using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Microsoft.AspNetCore.Identity;

namespace GameForge.Controllers
{
    public class AnswerController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public AnswerController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // GET: Answer
        public async Task<IActionResult> Index()
        {
            var gameForgeContext = _context.Answer.Include(a => a.Question).Include(a => a.User);
            return View(await gameForgeContext.ToListAsync());
        }

        // GET: Answer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .Include(a => a.Question)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answer/Create/QuestionID
        [HttpGet]
        public async Task<IActionResult> Create(int QuestionID)
        {
            var userId = await GetCurrentUserIdAsync();
            var AnswerCreate = new AnswerCreateViewModel { QuestionID = QuestionID, CanCreate = false };
            var latestAnswer = await _context.Answer.FirstOrDefaultAsync(m => m.QuestionID == QuestionID && m.UserID == userId);
            if (latestAnswer != null)
            {
                return Problem("You already Answered.");
            }
            return View(AnswerCreate);
        }

        // POST: Answer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionID", "AnswerText")] AnswerCreateViewModel answerDat)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetCurrentUserIdAsync();
                var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userId);
                var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == answerDat.QuestionID);

                if (question == null || user == null)
                {
                    return NotFound();
                }

                var answer = new Answer
                {
                    Question = question,
                    User = user,
                    CreationDate = DateTime.UtcNow,
                    LastEditTime = DateTime.UtcNow,
                    Upvotes = 0,
                    Downvotes = 0,
                    AnswerText = answerDat.AnswerText
                };
                question.NumberOfAnswers += 1;
                _context.Add(answer);
                _context.Update(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Question", new { id = question.QuestionID });
            }
            return View(answerDat);
        }

        // GET: Answer/Edit/5
        public async Task<IActionResult> Edit(int QuestionID, string UserID)
        {
            var answer = await _context.Answer.FirstOrDefaultAsync(m => m.QuestionID == QuestionID && m.UserID == UserID);
            if (answer == null)
            {
                return NotFound();
            }
            var answerEditViewModel = new AnswerEditViewModel { QuestionID = QuestionID, UserID = UserID, AnswerText = answer.AnswerText };
            var timeSpan = DateTime.UtcNow - answer.LastEditTime;
            if (timeSpan.TotalMinutes > 1)
            {
                answerEditViewModel.CanEdit = false;
            }
            return View(answerEditViewModel);
        }

        // POST: Answer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("QuestionID,UserID,AnswerText")] AnswerEditViewModel answerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var answer = await _context.Answer.FirstOrDefaultAsync(m => m.QuestionID == answerEditViewModel.QuestionID && m.UserID == answerEditViewModel.UserID);
                    if (answer == null)
                    {
                        return NotFound();
                    }

                    answer.AnswerText = answerEditViewModel.AnswerText;
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answerEditViewModel.QuestionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Question", new { id = answerEditViewModel.QuestionID });
            }
            return View(answerEditViewModel);
        }

        // GET: Answer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .Include(a => a.Question)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answer.FindAsync(id);
            if (answer != null)
            {
                _context.Answer.Remove(answer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.QuestionID == id);
        }
        [HttpPost]
        public async Task<IActionResult> AnswerVote([FromBody] AnswerVoteAction answerVoteAction)
        {

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == answerVoteAction.UserID);
            if (user == null) return NotFound();

            var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == answerVoteAction.QuestionID);
            if (question == null) return NotFound();

            var answer = await _context.Answer.FirstOrDefaultAsync(m => m.UserID == answerVoteAction.UserID && m.QuestionID == answerVoteAction.QuestionID);
            if (answer == null) return NotFound();

            var existingVote = await _context.AnswerVotes.FirstOrDefaultAsync(m => m.QuestionID == answerVoteAction.QuestionID && m.UserID == answerVoteAction.UserID);

            if (existingVote != null)
            {
                if (answerVoteAction.Type == existingVote.IsUpvote)
                {
                    return Json(new
                    {
                        success = false,
                        message = "You have already voted on this answer"
                    });
                }
                if (answerVoteAction.Type == true && existingVote.IsUpvote == false)
                {
                    answer.Upvotes += 1;
                    answer.Downvotes = answer.Downvotes == 0 ? 0 : (answer.Downvotes - 1);
                }
                else if (answerVoteAction.Type == false && existingVote.IsUpvote == true)
                {
                    answer.Upvotes = answer.Upvotes == 0 ? 0 : (answer.Upvotes - 1);
                    answer.Downvotes += 1;
                }

                existingVote.IsUpvote = !existingVote.IsUpvote;
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    upvotes = answer.Upvotes,
                    downvotes = answer.Downvotes
                });
            }
            if (answerVoteAction.Type == true)
            {
                answer.Upvotes += 1;
            }
            else if (answerVoteAction.Type == false)
            {
                answer.Downvotes += 1;
            }
            _context.AnswerVotes.Add(new AnswerVote { User = user, Question = question, IsUpvote = answerVoteAction.Type });

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                upvotes = answer.Upvotes,
                downvotes = answer.Downvotes
            });
        }
    }
}
