using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHause.Data;
using WareHause.Models;

namespace WareHause.Controllers
{
    public class MilkProductController : Controller
    {
        private readonly AnbarDbContext _context;
        public MilkProductController(AnbarDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<MIlkProduct> products = await _context.MIlkProducts.ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MIlkProduct product)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Fruits.AnyAsync(r => r.Endtime == product.Endtime);
            if (result)
            {
                ModelState.AddModelError("Endtime", "Məhsul vaxtını Dəyişin");
                return View();
            }
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id) 
        {
            if (id == null || id < 1) return BadRequest();

            MIlkProduct old = await _context.MIlkProducts.FirstOrDefaultAsync();
            if (old == null) return NotFound();
        
            return View(old);
                  
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,MIlkProduct product)
        {

            if (id == null || id < 1) return BadRequest();

            MIlkProduct old = await _context.MIlkProducts.FirstOrDefaultAsync();
            if (old == null) return NotFound();

            bool result = await _context.MIlkProducts.AnyAsync(r => r.Number == product.Number);
            if(result)
            {
                ModelState.AddModelError("Number", "Məhsul sayıni dəyişin");
                return View();
            }

            old.Number = product.Number;
            old.Name = product.Name;
            old.Endtime = product.Endtime;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id) 
        {
            if (id == null || id < 1) return BadRequest();

            MIlkProduct old = await _context.MIlkProducts.FirstOrDefaultAsync();
            if (old == null) return NotFound();

            _context.MIlkProducts.Remove(old);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }



    }
}
