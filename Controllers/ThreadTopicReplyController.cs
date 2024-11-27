using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Diagnostics;

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
        public async Task<IActionResult> Create(int ThreadTopicID, int? ParentReplyID)
        {
            var threadReply = new ThreadReplyCreateViewModel
            {
                ThreadTopicID = ThreadTopicID,
                ParentReplyID = ParentReplyID
            };
            var latestThreadReply = await _context.ThreadTopicReplies.OrderByDescending(m => m.CreationDate).FirstOrDefaultAsync(m => m.UserID == 1 && m.ThreadTopicID == ThreadTopicID);
            if (latestThreadReply != null)
            {
                var timeSpan = DateTime.UtcNow - latestThreadReply.CreationDate;
                if (timeSpan.TotalMinutes < 1)
                {
                    threadReply.CanCreate = false;
                }
            }
            return View(threadReply);
        }

        // POST: ThreadTopicReply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadTopicID,ThreadTopicReplyText,ParentReplyID")] ThreadReplyCreateViewModel threadReplyCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(m => m.Id == 1);
                if (user == null)
                {
                    return NotFound();
                }
                var threadTopic = await _context.ThreadTopic.FirstOrDefaultAsync(m => m.ThreadTopicID == threadReplyCreateViewModel.ThreadTopicID);
                if (threadTopic == null)
                {
                    return NotFound();
                }
                var parentReply = await _context.ThreadTopicReplies.FindAsync(threadReplyCreateViewModel.ParentReplyID);
                var threadReply = new ThreadTopicReply
                {
                    Message = threadReplyCreateViewModel.ThreadTopicReplyText,
                    CreationDate = DateTime.UtcNow,
                    LastEditTime=DateTime.UtcNow,
                    User = user,
                    ThreadTopic = threadTopic,
                    ParentReply = parentReply,
                };
                _context.Add(threadReply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threadReplyCreateViewModel);
        }

        // GET: ThreadTopicReply/Edit/5
        public async Task<IActionResult> Edit(int ThreadTopicReplyID)
        {
            var threadTopicReply = await _context.ThreadTopicReplies.FindAsync(ThreadTopicReplyID);
            if (threadTopicReply == null)
            {
                return NotFound();
            }
            var threadReplyEditModel = new ThreadReplyEditViewModel
            {
                ThreadTopicID = threadTopicReply.ThreadTopicReplyID,
                ThreadTopicReplyText = threadTopicReply.Message,
                //ParentReplyID = threadTopicReply.ParentReplyID
            };
            var timeSpan = DateTime.UtcNow - threadTopicReply.LastEditTime;
            if (timeSpan.TotalMinutes > 1)
            {
                threadReplyEditModel.CanEdit = false;
            }
            return View(threadReplyEditModel);
        }

        // POST: ThreadTopicReply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ThreadTopicID,ThreadTopicReplyID,ThreadTopicReplyText")] ThreadReplyEditViewModel threadTopicEditReply)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var threadreply = await _context.ThreadTopicReplies.FindAsync(threadTopicEditReply.ThreadTopicID);
                    if (threadreply == null)
                    {
                        return NotFound();
                    }
                    threadreply.Message = threadTopicEditReply.ThreadTopicReplyText;
                    threadreply.LastEditTime = DateTime.UtcNow;
                    _context.Update(threadreply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadTopicReplyExists(threadTopicEditReply.ThreadTopicID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "ThreadTopic", new { id = threadTopicEditReply.ThreadTopicID });
            }
            return View(threadTopicEditReply);
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
