using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;

namespace OganiShoppingProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly ShoppingDbContext _context;
        public BasketController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Basket> basket = _context.Baskets.ToList();
            return View(basket);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Basket basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id == id);
            if (basket == null) return NotFound();

            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
