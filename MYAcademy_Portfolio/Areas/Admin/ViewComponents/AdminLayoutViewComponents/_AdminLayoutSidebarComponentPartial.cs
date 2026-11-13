using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutSidebarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutSidebarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.UnreadMessageCount = _context.Messages.Count(x => !x.IsRead);
            return View();
        }
    }
}
