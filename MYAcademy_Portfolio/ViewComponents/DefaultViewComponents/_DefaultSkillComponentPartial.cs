using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Skills
                .Include(x => x.SkillItems)
                .OrderBy(x => x.SkillId)
                .ToList();
            return View(values);
        }
    }
}
