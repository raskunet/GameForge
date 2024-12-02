using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Mono.TextTemplating;
namespace GameForge.Controllers
{
    public class ForumController : Controller
    {
        private readonly GameForgeContext _context;

        public ForumController(GameForgeContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var questions = _context.Question.OrderBy(q => q.CreationDate);
            var threadTopics = _context.ThreadTopic.OrderBy(q => q.CreationDate);

            var qPage = await PaginatedList<Question>.CreateAsync(questions, 1, 10);
            var tPage = await PaginatedList<ThreadTopic>.CreateAsync(threadTopics, 1, 10);

            var forumPage = new Forum
            {
                ThreadTopics = tPage,
                Questions = qPage
            };
            return View(forumPage);

        }
    }
}