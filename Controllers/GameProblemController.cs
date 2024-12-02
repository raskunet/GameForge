using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using NuGet.Versioning;
using Microsoft.AspNetCore.Identity;

namespace GameForge.Controllers
{
    public class GameProblemController : Controller
    {
        private readonly GameForgeContext _context;
        private readonly UserManager<User> _userManager;

        public GameProblemController(GameForgeContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // GET: GameProblem
        public async Task<IActionResult> Index(int GameID)
        {
            var games = _context.GameProblems.Where(m => m.GameID == GameID);

            return View(await games.ToListAsync());
        }

        // GET: GameProblem/Details/5
        public async Task<IActionResult> Details(int GameProblemID)
        {
            var gameProblem = await _context.GameProblems
                .Include(g => g.Game)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.GameProblemID == GameProblemID);
            if (gameProblem == null)
            {
                return Problem("Not found");
            }

            return View(gameProblem);
        }

        // GET: GameProblem/Create
        public async Task<IActionResult> Create(int GameID)
        {
            var game = await _context.Game.FirstOrDefaultAsync(m => m.Id == GameID);
            if (game == null) {
                return NotFound();
            }
            var gameProblemCreateVM = new GameProblemCreateVM
            {
                GameID = GameID
            };
            return View(gameProblemCreateVM);
        }

        // POST: GameProblem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,GameID,ProblemTitle,ProblemDescription")] GameProblemCreateVM gameProblemVM)
        {
            if (ModelState.IsValid)
            {
                var userID=await GetCurrentUserIdAsync();
                var user = await _context.User.FirstOrDefaultAsync(m => m.Id == userID);
                if (user == null) {
                    return NotFound();
                }
                var game = await _context.Game.FirstOrDefaultAsync(m => m.Id == gameProblemVM.GameID);
                if (game == null) {
                    return NotFound();
                }
                var gameProblem = new GameProblem
                {
                    Game = game,
                    User = user,
                    Title = gameProblemVM.ProblemTitle,
                    ProblemDescription = gameProblemVM.ProblemDescription,
                    CreationDate=DateTime.UtcNow
                };
                _context.Add(gameProblem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Games");
            }
            return View(gameProblemVM);
        }

        private bool GameProblemExists(int id)
        {
            return _context.GameProblems.Any(e => e.GameProblemID == id);
        }
    }
}
