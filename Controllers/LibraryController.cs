using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;

namespace GameForge.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GameForgeContext _context;

        public LibraryController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: Library/[Action]
        [HttpGet("{id}")]
        public  ActionResult Index(int id=1)
        {
            // var library = await _context.Library
            //     .Include(l => l.DownloadedGames)
            //     .FirstOrDefaultAsync(l => l.UserID == 1);

            // if (library == null)
            //     return NotFound($"Library with ID {id} not found.");

            return View();
        
        }
        //POST: Library/Details
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Details([FromBody] Game game)
        // {
        //     if (game == null)
        //         return BadRequest("Game data is required.");

        //     _context.Game.Add(game);
        //     await _context.SaveChangesAsync();
        //     return View();
        // }
        
    }
}