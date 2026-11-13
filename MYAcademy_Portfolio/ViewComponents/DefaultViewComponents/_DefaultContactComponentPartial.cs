using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultWiewComponents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Contact = _context.Contacts.OrderBy(x => x.ContactId).FirstOrDefault();
            ViewBag.SocialMedias = _context.SocialMedias.OrderBy(x => x.SocialMediaId).ToList();
            return View();
        }
    }
}
