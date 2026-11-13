using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Models;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;
        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.SkillItems.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessageCount = _context.Messages.Count(),
                UnreadMessageCount = _context.Messages.Count(x => !x.IsRead),

                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),

                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(3)
                    .ToList()
            };

            var projectCounts = _context.Projects
                .GroupBy(x => x.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToList();

            model.CategorySummaries = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList()
                .Select(c => new CategoryProjectCount
                {
                    CategoryName = c.CategoryName,
                    ProjectCount = projectCounts.FirstOrDefault(p => p.CategoryId == c.CategoryId)?.Count ?? 0
                })
                .ToList();

            return View(model);
        }
    }
}
