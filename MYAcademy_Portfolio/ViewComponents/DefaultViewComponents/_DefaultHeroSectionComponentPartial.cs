using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.HeroSections.FirstOrDefault();
            ViewBag.greeting = value?.Greeting ?? "Merhaba";
            ViewBag.title = value?.Title ?? "";
            ViewBag.description = value?.Description ?? "";
            ViewBag.mainImageUrl = value?.MainImageUrl;
            ViewBag.experienceYear = value?.ExperienceYear ?? "2+";
            ViewBag.studentCount = value?.StudentCount;
            ViewBag.SocialMedias = _context.SocialMedias.OrderBy(x => x.SocialMediaId).ToList();
            ViewBag.HeroTitles = _context.HeroTitles
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Select(x => x.Title)
                .ToList();
            return View();
        }
    }
}
