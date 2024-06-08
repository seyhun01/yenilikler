using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;

namespace OganiShoppingProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly ShoppingDbContext _context;
        public ContactController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Contact contact = _context.Contacts.FirstOrDefault();
            return View(contact);
        }
        public IActionResult Messagee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Messagee(Message message)
        {
            if (!ModelState.IsValid) return View();

            if (message == null) return BadRequest();

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }
    }
}
