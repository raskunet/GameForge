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
        public async Task<IActionResult> Index()
        {
            var gameForgeContext = _context.ThreadTopic.Include(t => t.User);
            return View(await gameForgeContext.ToListAsync());
        }

        // GET: ThreadTopic/Details/5
        public async Task<IActionResult> Details(int  id)
        {
            var threadTopic = await _context.ThreadTopic
                .Include(t => t.User)
                .Include(t=>t.ThreadTopidcReplies)
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
                var user = await _context.User.FirstOrDefaultAsync(m => m.ID == 1);
                if(user==null){
                    return NotFound();
                }
                var thread = new ThreadTopic 
                { 
                    User = user, 
                    Title = threadCreateViewModel.Title, 
                    Message = threadCreateViewModel.Message, 
                    CreationDate = DateTime.UtcNow, 
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
