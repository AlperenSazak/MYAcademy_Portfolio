using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Experiences.Where(x => x.IsActive).OrderByDescending(x => x.ExperienceId).ToList();
            return View(values);
        }
    }
}
