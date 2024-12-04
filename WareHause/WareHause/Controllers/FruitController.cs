using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHause.Data;
using WareHause.Models;

namespace WareHause.Controllers
{
    public class FruitController : Controller
    {
        private readonly AnbarDbContext _context;
        public FruitController(AnbarDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Fruit> fruits = await _context.Fruits.ToListAsync();
            return View(fruits);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Fruit fruit)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Fruits.AnyAsync(r => r.Endtime == fruit.Endtime);

            if(result)
            {
                ModelState.AddModelError("Endtime", "Məhsul üçün yeni tarix daxil edin ");
                return View();
            }
            await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        } 
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Fruit old = await _context.Fruits.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();

            return View(old);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Fruit fruit)
        {
            if (id == null || id < 1) return BadRequest();
            Fruit old = await _context.Fruits.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();

            bool result = await _context.Fruits.AnyAsync(o => o.Number == fruit.Number);

            if(result)
            {
                ModelState.AddModelError("Number", "Məhsul sayını dəyişin");
                return View();
            }

            old.Name = fruit.Name;
            old.Number = fruit.Number;
            old.Endtime = fruit.Endtime;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Fruit old = await _context.Fruits.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();

            _context.Fruits.Remove(old);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
