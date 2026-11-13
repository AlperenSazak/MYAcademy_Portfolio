using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultEducationComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultEducationComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Educations
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.EducationId)
                .ToList();
            return View(values);
        }
    }
}
