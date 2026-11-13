using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;
        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList(string? search, string? status)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.SenderName.Contains(search) || x.Subject.Contains(search) || x.SenderMail.Contains(search));
            }

            if (status == "unread")
            {
                query = query.Where(x => !x.IsRead);
            }
            else if (status == "read")
            {
                query = query.Where(x => x.IsRead);
            }

            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.TotalCount = _context.Messages.Count();
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);

            return View(query.OrderByDescending(x => x.SendDate).ToList());
        }

        [HttpGet]
        public IActionResult GetMessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            if (!value.IsRead)
            {
                value.IsRead = true;
                _context.SaveChanges();
            }

            return Json(new
            {
                id = value.MessageId,
                senderName = value.SenderName,
                senderMail = value.SenderMail,
                subject = value.Subject,
                messageDetail = value.MessageDetail,
                sendDate = value.SendDate.ToString("dd MMMM yyyy, HH:mm"),
                unreadCount = _context.Messages.Count(x => !x.IsRead)
            });
        }

        public IActionResult ToggleReadStatus(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                value.IsRead = !value.IsRead;
                _context.SaveChanges();
                TempData["Success"] = value.IsRead ? "Mesaj okundu olarak işaretlendi." : "Mesaj okunmadı olarak işaretlendi.";
            }
            return RedirectToAction("MessageList");
        }

        public IActionResult MarkAllAsRead()
        {
            var unreadMessages = _context.Messages.Where(x => !x.IsRead).ToList();
            foreach (var item in unreadMessages)
            {
                item.IsRead = true;
            }
            _context.SaveChanges();
            TempData["Success"] = $"{unreadMessages.Count} mesaj okundu olarak işaretlendi.";
            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Mesaj silindi.";
            }
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            TempData["Success"] = "Test mesajı oluşturuldu.";
            return RedirectToAction("MessageList");
        }
    }
}
