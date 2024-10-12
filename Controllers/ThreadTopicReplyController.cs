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
    public class ThreadTopicReplyController : Controller
    {
        private readonly GameForgeContext _context;

        public ThreadTopicReplyController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: ThreadTopicReply
        public async Task<IActionResult> Index()
        {
            var gameForgeContext = _context.ThreadTopicReplies.Include(t => t.ThreadTopic);
            return View(await gameForgeContext.ToListAsync());
        }

        // GET: ThreadTopicReply/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopicReply = await _context.ThreadTopicReplies
                .Include(t => t.ThreadTopic)
                .FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (threadTopicReply == null)
            {
                return NotFound();
            }

            return View(threadTopicReply);
        }

        // GET: ThreadTopicReply/Create
        public IActionResult Create()
        {
            ViewData["ThreadTopicID"] = new SelectList(_context.ThreadTopic, "ThreadTopicID", "ThreadTopicID");
            return View();
        }

        // POST: ThreadTopicReply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadTopicID,Message,UserID,CreationDate")] ThreadTopicReply threadTopicReply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(threadTopicReply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThreadTopicID"] = new SelectList(_context.ThreadTopic, "ThreadTopicID", "ThreadTopicID", threadTopicReply.ThreadTopicID);
            return View(threadTopicReply);
        }

        // GET: ThreadTopicReply/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopicReply = await _context.ThreadTopicReplies.FindAsync(id);
            if (threadTopicReply == null)
            {
                return NotFound();
            }
            ViewData["ThreadTopicID"] = new SelectList(_context.ThreadTopic, "ThreadTopicID", "ThreadTopicID", threadTopicReply.ThreadTopicID);
            return View(threadTopicReply);
        }

        // POST: ThreadTopicReply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThreadTopicID,Message,UserID,CreationDate")] ThreadTopicReply threadTopicReply)
        {
            if (id != threadTopicReply.ThreadTopicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(threadTopicReply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadTopicReplyExists(threadTopicReply.ThreadTopicID))
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
            ViewData["ThreadTopicID"] = new SelectList(_context.ThreadTopic, "ThreadTopicID", "ThreadTopicID", threadTopicReply.ThreadTopicID);
            return View(threadTopicReply);
        }

        // GET: ThreadTopicReply/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadTopicReply = await _context.ThreadTopicReplies
                .Include(t => t.ThreadTopic)
                .FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (threadTopicReply == null)
            {
                return NotFound();
            }

            return View(threadTopicReply);
        }

        // POST: ThreadTopicReply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var threadTopicReply = await _context.ThreadTopicReplies.FindAsync(id);
            if (threadTopicReply != null)
            {
                _context.ThreadTopicReplies.Remove(threadTopicReply);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadTopicReplyExists(int id)
        {
            return _context.ThreadTopicReplies.Any(e => e.ThreadTopicID == id);
        }
    }
}
