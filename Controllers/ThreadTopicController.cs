using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;

namespace GameForge.Controllers
{
    public class ThreadTopicController : Controller
    {
        private readonly GameForgeContext _context;

        public ThreadTopicController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: ThreadTopic
        public async Task<IActionResult> Index()
        {
            var gameForgeContext = _context.ThreadTopic.Include(t => t.User);
            return View(await gameForgeContext.ToListAsync());
        }

        // GET: ThreadTopic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopic = await _context.ThreadTopic
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (threadTopic == null)
            {
                return NotFound();
            }

            return View(threadTopic);
        }

        // GET: ThreadTopic/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID");
            return View();
        }

        // POST: ThreadTopic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadTopicID,UserID,Title,CreationDate,Message,Tag,LatestReplyID,LatestReplyTime,NumberOfReplies")] ThreadTopic threadTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(threadTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", threadTopic.UserID);
            return View(threadTopic);
        }

        // GET: ThreadTopic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopic = await _context.ThreadTopic.FindAsync(id);
            if (threadTopic == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", threadTopic.UserID);
            return View(threadTopic);
        }

        // POST: ThreadTopic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThreadTopicID,UserID,Title,CreationDate,Message,Tag,LatestReplyID,LatestReplyTime,NumberOfReplies")] ThreadTopic threadTopic)
        {
            if (id != threadTopic.ThreadTopicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(threadTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadTopicExists(threadTopic.ThreadTopicID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", threadTopic.UserID);
            return View(threadTopic);
        }

        // GET: ThreadTopic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopic = await _context.ThreadTopic
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (threadTopic == null)
            {
                return NotFound();
            }

            return View(threadTopic);
        }

        // POST: ThreadTopic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var threadTopic = await _context.ThreadTopic.FindAsync(id);
            if (threadTopic != null)
            {
                _context.ThreadTopic.Remove(threadTopic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadTopicExists(int id)
        {
            return _context.ThreadTopic.Any(e => e.ThreadTopicID == id);
        }
    }
}
