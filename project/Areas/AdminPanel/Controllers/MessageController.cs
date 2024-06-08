using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class MessageController : Controller
    {
        private readonly ShoppingDbContext _context;
        public MessageController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 0)
        {
            List<Message> messages = _context.Messages.Skip(page * 5).Take(5).ToList();

            PaginationVM<Message> pagination = new PaginationVM<Message>
            {
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Messages.Count() / 4),
                Items = messages
            };


            return View(pagination);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Message message = _context.Messages.FirstOrDefault(m => m.Id == id);

            if (message == null) return NotFound();
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
