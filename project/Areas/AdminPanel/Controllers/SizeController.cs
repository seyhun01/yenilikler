using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SizeController : Controller
    {
        private readonly ShoppingDbContext _context;
        public SizeController(ShoppingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 0)
        {
            List<Sizes> size = _context.Sizess.Skip(page * 5).Take(5).ToList();
            PaginationVM<Sizes> pagination = new PaginationVM<Sizes>
            {
                Items = size,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Sizess.Count() / 4)

            };
            return View(pagination);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sizes sizes)
        {
            if (!ModelState.IsValid) return View();



            bool result = await _context.Sizess.AnyAsync(s => s.Name.Trim().ToLower() == sizes.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Movcuddur");
                return View();
            }
            await _context.Sizess.AddAsync(sizes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Sizes old = await _context.Sizess.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();
            return View(old);


        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Sizes sizes)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Sizes old = await _context.Sizess.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();


            if (!ModelState.IsValid) return View();

            if (old.Name == sizes.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            bool result = await _context.Sizess.AnyAsync(s => s.Name.ToLower().Trim() == sizes.Name.ToLower().Trim() && s.Id != old.Id);
            if (result)
            {
                ModelState.AddModelError("Name", "Movcuddur");
                return View(old);

            }
            old.Name = sizes.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Sizes sizes = await _context.Sizess.FirstOrDefaultAsync(s => s.Id == id);
            if (id == null) NotFound();
            _context.Sizess.Remove(sizes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
