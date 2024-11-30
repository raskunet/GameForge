using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Amazon.SecurityToken.Model;

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
        public async Task<IActionResult> Index(string ThreadTag, string ThreadSearchString)
        {
            if (_context.ThreadTopic == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }


            IQueryable<string> tagQuery = from t in _context.ThreadTags select t.TagName;


            var threadTopics = from m in _context.ThreadTopic
                                select m;
            

            if (!string.IsNullOrEmpty(ThreadSearchString))
            {
                threadTopics = threadTopics.Where(s => s.Title!.ToUpper().Contains(ThreadSearchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(ThreadTag))
            {
                threadTopics = threadTopics.Where(x => x.Tag.Contains(ThreadTag));
            }

            var threadSearchviewModel = new ThreadSearchViewModel
            {
                Tags = new SelectList(await tagQuery.ToListAsync()),
                ThreadTopics = new(await threadTopics.ToListAsync())
            };

            return View(threadSearchviewModel);
        }

        // GET: ThreadTopic/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var threadTopic = await _context.ThreadTopic
                .Include(t => t.User)
                .Include(t => t.ThreadTopidcReplies)
                .FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (threadTopic == null)
            {
                return NotFound();
            }

            var threadpostViewModel = new ThreadPost
            {
                ThreadTopic = threadTopic,
                DiscussFlag = false
            };

            return View(threadpostViewModel);
        }

        // GET: ThreadTopic/Create
        public async Task<IActionResult> Create()
        {
            var threadTopicCreate = new ThreadCreateViewModel();
            var lastThreadTopic = await _context.ThreadTopic.OrderByDescending(m => m.CreationDate).FirstOrDefaultAsync(m => m.UserID == 1);
            if (lastThreadTopic != null)
            {
                var timeSpan = DateTime.UtcNow - lastThreadTopic.CreationDate;
                if (timeSpan.TotalMinutes < 1)
                {
                    // If one minute has passed since the user last created a thread, we now allow the user to create a new thread.
                    threadTopicCreate.CanCreate = false;
                }
            }
            var tags = await _context.ThreadTags.ToListAsync();
            threadTopicCreate.SelectTags = new SelectList(tags.Select(l => l.TagName).ToList());
            return View(threadTopicCreate);
        }

        // POST: ThreadTopic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Message,Tag")] ThreadCreateViewModel threadCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(m => m.Id == 1);
                if (user == null)
                {
                    return NotFound();
                }
                var thread = new ThreadTopic
                {
                    User = user,
                    Title = threadCreateViewModel.Title,
                    Message = threadCreateViewModel.Message,
                    CreationDate = DateTime.UtcNow,
                    LastEditTime = DateTime.UtcNow,
                    Tag = threadCreateViewModel.Tag
                };
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threadCreateViewModel);
        }

        // GET: ThreadTopic/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var threadTopic = await _context.ThreadTopic.FindAsync(id);
            if (threadTopic == null)
            {
                return NotFound();
            }
            var threadEditViewModel = new ThreadEditViewModel
            {
                Title = threadTopic.Title,
                ThreadTopicID = threadTopic.ThreadTopicID,
                Message = threadTopic.Message,
                Tag = threadTopic.Tag,
                SelectTags = new SelectList((await _context.ThreadTags.ToListAsync()).Select(l => l.TagName).ToList())
            };
            var latestThreadTopicEdit = await _context.ThreadTopic.OrderByDescending(m => m.LastEditTime).FirstOrDefaultAsync(m => m.ThreadTopicID == id);
            if (latestThreadTopicEdit != null)
            {
                var timeSpan = DateTime.UtcNow - latestThreadTopicEdit.LastEditTime;
                if (timeSpan.TotalMinutes < 1)
                {
                    threadEditViewModel.CanEdit = false;
                }
            }
            return View(threadEditViewModel);
        }

        // POST: ThreadTopic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ThreadTopicID,Title,Message,Tag")] ThreadEditViewModel threadEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var threadTopic = await _context.ThreadTopic.FirstOrDefaultAsync(m => m.ThreadTopicID == threadEditViewModel.ThreadTopicID);
                    if (threadTopic == null)
                    {
                        return NotFound();
                    }
                    threadTopic.Message = threadEditViewModel.Message;
                    threadTopic.Title = threadEditViewModel.Title;
                    threadTopic.Tag = threadEditViewModel.Tag;
                    threadTopic.LastEditTime = DateTime.UtcNow;
                    _context.Update(threadTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadTopicExists(threadEditViewModel.ThreadTopicID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = threadEditViewModel.ThreadTopicID });
            }
            return View(threadEditViewModel);
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