using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Mono.TextTemplating;
namespace GameForge.Controllers
{
    public class QuestionController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public QuestionController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return "";
            }
            return user.Id;
        }
        // GET: Question
        public async Task<IActionResult> Index(string QuestionSearchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (_context.Question == null)
            {
                return Problem("Entity Set `GameForge.Models.Question` is null");
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            //ViewData["VoteSortParam"] = sortOrder == "up" ? "down" : "up";
            ViewData["NumAnswerSortParam"] = sortOrder == "more" ? "less" : "more";


            if (QuestionSearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                QuestionSearchString = currentFilter;
            }
            @ViewData["CurrentFilter"] = QuestionSearchString;

            var questions = from q in _context.Question
                            select q;

            if (!string.IsNullOrEmpty(QuestionSearchString))
            {
                questions = questions.Where(w => w.Title.ToUpper().Contains(QuestionSearchString.ToUpper()));
            }

            questions = sortOrder switch
            {
                "date_asc" => questions.OrderBy(m => m.CreationDate),
                "date_desc" => questions.OrderByDescending(m => m.CreationDate),
                "more" => questions.OrderBy(m => m.NumberOfAnswers),
                "less" => questions.OrderByDescending(m => m.NumberOfAnswers),
                _ => questions.OrderBy(m => m.Title),
            };
            int pageSize = 3;

            return View(await PaginatedList<Question>.CreateAsync(questions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userID = await GetCurrentUserIdAsync();
            if (userID == "")
            {
                return NotFound("NO User Found");
            }
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);
            var answerFlag = false;
            var modifyFlag = false;
            var question = await _context.Question
                .Include(q => q.User)
                .Include(q => q.Answers)
                .ThenInclude(a=>a.User)
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
            if (question.AuthorID == userID)
            {
                modifyFlag = true;
            }
            var questionPost = new QuestionPost { Question = question, AnswerFlag = answerFlag, ModifyFlag = modifyFlag };

            return View(questionPost);
        }

        // GET: Question/Create
        public async Task<IActionResult> Create()
        {
            var userID = await GetCurrentUserIdAsync();
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);

            var QuestionCreate = new QuestionCreateViewModel();
            var LatestQuestion = await _context.Question.OrderByDescending(m => m.CreationDate).FirstOrDefaultAsync(m => m.AuthorID == userID);
            if (LatestQuestion != null)
            {
                var timeSpan = DateTime.UtcNow - LatestQuestion.CreationDate;
                if (timeSpan.TotalMinutes < 1)
                {
                    QuestionCreate.CanCreate = false;
                }
            }

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
                var userId = await GetCurrentUserIdAsync();
                var tempUser = await _context.User.FirstOrDefaultAsync(m => m.Id == userId);
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
                    CreationDate = DateTime.UtcNow,
                    LastEditTime = DateTime.UtcNow
                };
                _context.Add(question);
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
            var questionEditViewModel = new QuestionEditViewModel
            {
                QuestionID = question.QuestionID,
                QuestionText = question.QuestionText,
                Title = question.Title
            };
            var userID = await GetCurrentUserIdAsync();
            if (userID == "")
            {
                return NotFound();
            }
            if (question.AuthorID != userID)
            {
                return Unauthorized();
            }
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);

            var LatestEditQuestion = await _context.Question.FirstOrDefaultAsync(m => m.AuthorID == userID && m.QuestionID == id);
            if (LatestEditQuestion != null)
            {
                var timeS = DateTime.UtcNow - LatestEditQuestion.LastEditTime;
                if (timeS.TotalMinutes < 1)
                {
                    questionEditViewModel.CanEdit = false;
                }
            }
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
                    var userID = await GetCurrentUserIdAsync();
                    var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);
                    var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == questionEditViewModel.QuestionID && m.User.Id == userID);
                    
                    if (question == null)
                    {
                        return NotFound();
                    }

                    if (userID == "")
                    {
                        return NotFound();
                    }
                    
                    if (question.AuthorID != userID)
                    {
                        return Unauthorized();
                    }
                    question.Title = questionEditViewModel.Title;
                    question.QuestionText = questionEditViewModel.QuestionText;
                    question.LastEditTime = DateTime.UtcNow;
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
            var userID = await GetCurrentUserIdAsync();
            if (userID == "")
            {
                return NotFound();
            }
            if (question.AuthorID != userID)
            {
                return Unauthorized();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            var userID = await GetCurrentUserIdAsync();
            if (userID == "")
            {
                return NotFound();
            }
            if (userID != question.AuthorID)
            {
                return Unauthorized();
            }
            _context.Question.Remove(question);

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
            var userID = await GetCurrentUserIdAsync();
            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);
            if (user == null)
            {
                return NotFound();
            }
            var question = await _context.Question.FirstOrDefaultAsync(m => m.QuestionID == questionVoteAction.QuestionID);
            if (question == null) return NotFound();



            var existingVote = await _context.QuestionVotes
                .FirstOrDefaultAsync(v => v.QuestionID == questionVoteAction.QuestionID && v.UserID == userID);
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
