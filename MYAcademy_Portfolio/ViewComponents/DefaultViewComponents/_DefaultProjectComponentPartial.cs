using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            ViewBag.Categories = values
                .Where(x => x.Category != null)
                .Select(x => x.Category!)
                .GroupBy(x => x.CategoryId)
                .Select(g => g.First())
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View(values);
        }
    }
}
