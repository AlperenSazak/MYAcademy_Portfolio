using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var value = _context.Abouts.FirstOrDefault();

            ViewBag.Hero = _context.HeroSections.FirstOrDefault();
            ViewBag.ActiveProjectCount = _context.Projects.Count(x => x.IsActive);

            return View(value);
        }
    }
}
