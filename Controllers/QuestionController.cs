using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Markdig;
namespace GameForge.Controllers
{
    public class QuestionController : Controller
    {
        private readonly GameForgeContext _context;

        public QuestionController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            var gameForgeContext = _context.Question.Include(q => q.User);
            return View(await gameForgeContext.ToListAsync());
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userID = 1;
            var answerFlag = false;
            var question = await _context.Question
                .Include(q => q.User)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }
            var answer = await _context.Answer.FirstOrDefaultAsync(m => m.UserID == userID && m.QuestionID == id);
            if (answer != null)
            {
                answerFlag = true;
            }
            var questionPost = new QuestionPost { Question = question, AnswerFlag = answerFlag };

            return View(questionPost);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            var QuestionCreate = new QuestionCreateViewModel();
            return View(QuestionCreate);
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title", "QuestionText")] QuestionCreateViewModel questionDat)
        {
            if (ModelState.IsValid)
            {
                //TODO : Get Current User From saved Cookie and Use that instead of this 
                var tempUser = await _context.User.FirstOrDefaultAsync(m => m.Id == 1);
                if (tempUser == null)
                {
                    return NotFound();
                }
                Question question = new()
                {
                    User = tempUser,
                    Title = questionDat.Title,
                    QuestionText = questionDat.QuestionText,
                    Upvotes = 0,
                    Downvotes = 0,
                    NumberOfAnswers = 0,
                    LatestAnswerID = 0,
                    CreationDate = DateTime.UtcNow
                };
                _context.Add(question);
                Console.WriteLine(question);
                await _context.SaveChangesAsync();
                var LatestQuestionID = question.QuestionID;
                return RedirectToAction("Details", new { id = LatestQuestionID });
            }
            return View(questionDat);
        }

        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            var questionEditViewModel = new QuestionEditViewModel { QuestionID = question.QuestionID, QuestionText = question.QuestionText, Title = question.Title };
            return View(questionEditViewModel);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("QuestionID,Title,QuestionText")] QuestionEditViewModel questionEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userID = 1;
                    var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == questionEditViewModel.QuestionID && m.User.Id == userID);
                    if (question == null)
                    {
                        return NotFound();
                    }
                    question.Title = questionEditViewModel.Title;
                    question.QuestionText = questionEditViewModel.QuestionText;
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(questionEditViewModel.QuestionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = questionEditViewModel.QuestionID });
            }
            return View(questionEditViewModel);
        }

        // GET: Question/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.QuestionID == id);
        }

        // POST: Question/QuestionVote
        // Body :   { 
        //              QuestionID,
        //              Type
        //          }
        [HttpPost, ActionName("QuestionVote")]
        public async Task<IActionResult> QuestionVote([FromBody] QuestionVoteAction questionVoteAction)
        {
            Console.WriteLine("Here in QuestionVote");
            Console.WriteLine(questionVoteAction.QuestionID);
            Console.WriteLine(questionVoteAction.Type);

            var userId = 1; //GetCurrentUserId();
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == questionVoteAction.QuestionID);
            if (question == null) return NotFound();



            var existingVote = await _context.QuestionVotes
                .FirstOrDefaultAsync(v => v.QuestionID == questionVoteAction.QuestionID && v.UserID == userId);
            if (existingVote != null)
            {
                if (questionVoteAction.Type == existingVote.IsUpvote)
                {
                    return Json(new { success = false, message = "You have already voted on this item." });
                }
                if (questionVoteAction.Type == true && existingVote.IsUpvote == false)
                {
                    question.Upvotes += 1;
                    question.Downvotes = question.Downvotes == 0 ? 0 : (question.Downvotes - 1);
                }
                else if (questionVoteAction.Type == false && existingVote.IsUpvote == true)
                {
                    question.Upvotes = question.Upvotes == 0 ? 0 : (question.Upvotes - 1);
                    question.Downvotes += 1;
                }
                existingVote.IsUpvote = !existingVote.IsUpvote;

                Console.WriteLine(question.Upvotes);
                Console.WriteLine(question.Downvotes);

                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    upvotes = question.Upvotes,
                    downvotes = question.Downvotes
                });
            }



            if (questionVoteAction.Type == true)
            {
                question.Upvotes += 1;
            }
            else if (questionVoteAction.Type == false)
            {
                question.Downvotes += 1;
            }

            _context.QuestionVotes.Add(new QuestionVote { User = user, Question = question, IsUpvote = questionVoteAction.Type });

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                upvotes = question.Upvotes,
                downvotes = question.Downvotes
            });
        }
    }
}
