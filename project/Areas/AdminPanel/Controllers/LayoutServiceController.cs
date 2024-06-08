using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class LayoutServiceController : Controller
    {
        private readonly ShoppingDbContext _context;
        public LayoutServiceController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 0)
        {
            List<Settings> setting = _context.Settingss.Skip(page * 5).Take(5).ToList();
            PaginationVM<Settings> pagination = new PaginationVM<Settings>
            {
                Items = setting,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Settingss.Count() / 4)

            };


            return View(pagination);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Settings setting)
        {
            if (!ModelState.IsValid) return View();



            bool result = await _context.Settingss.AnyAsync(s => s.Value.Trim().ToLower() == setting.Value.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Value", "Movcuddur");
                return View();
            }
            await _context.Settingss.AddAsync(setting);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Settings old = await _context.Settingss.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();
            return View(old);


        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Settings setting)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Settings old = await _context.Settingss.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();


            if (!ModelState.IsValid) return View();

            if (old.Value == setting.Value)
            {
                return RedirectToAction(nameof(Index));
            }
            bool result = await _context.Settingss.AnyAsync(s => s.Value.ToLower().Trim() == setting.Value.ToLower().Trim() && s.Id != old.Id);
            if (result)
            {
                ModelState.AddModelError("Value", "Movcuddur");
                return View(old);

            }
            old.Value = setting.Value;
            old.Key = setting.Key;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Settings old = await _context.Settingss.FirstOrDefaultAsync(s => s.Id == id);
            if (id == null) NotFound();
            _context.Settingss.Remove(old);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}
