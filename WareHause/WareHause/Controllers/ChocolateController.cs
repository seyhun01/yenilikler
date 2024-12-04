using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHause.Data;
using WareHause.Models;

namespace WareHause.Controllers
{
    public class ChocolateController : Controller
    {
        private readonly AnbarDbContext _context;
        public ChocolateController(AnbarDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Chocolate> chocolates = await _context.Chocolates.ToListAsync();
            return View(chocolates);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Chocolate chocolate)
        {
            if (!ModelState.IsValid) return View();
            bool result = await _context.Chocolates.AnyAsync(c => c.Name.Trim().ToLower() == chocolate.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Istifade olunub");
                return View();
            }
            await _context.Chocolates.AddAsync(chocolate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id) 
        {
            if (id == null || id < 1) return BadRequest();

            Chocolate old = await _context.Chocolates.FirstOrDefaultAsync(o => o.Id == id);

            if (old == null) return NotFound();

            return View(old);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Chocolate chocolate)
        {
            if (id == null || id < 1) return BadRequest();

            Chocolate old = await _context.Chocolates.FirstOrDefaultAsync(o => o.Id == id);

            if (old == null) NotFound();

            bool result = await _context.Chocolates.AnyAsync(o=>o.Endtime == chocolate.Endtime);
            if (result)
            {
                ModelState.AddModelError("Endtime", "Tarixi Deyisin");
                return View();
            }
            old.Name = chocolate.Name;
            old.Endtime = chocolate.Endtime;
            old.Number = chocolate.Number;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Chocolate old = await _context.Chocolates.FirstOrDefaultAsync(o => o.Id == id);

            if (old == null) NotFound();

            _context.Chocolates.Remove(old);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

    }
}
