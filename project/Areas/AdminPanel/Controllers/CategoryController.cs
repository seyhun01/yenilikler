using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{


    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly ShoppingDbContext _context;
        public CategoryController(ShoppingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 0)
        {
            List<Category> category = _context.Categories.Skip(page * 5).Take(4).ToList();
            PaginationVM<Category> pagination = new PaginationVM<Category>
            {

                Items = category,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 4)

            };
            return View(pagination);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();



            bool result = await _context.Categories.AnyAsync(s => s.Name.Trim().ToLower() == category.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Movcuddur");
                return View();
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Category old = await _context.Categories.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();
            return View(old);


        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Category old = await _context.Categories.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null) return NotFound();


            if (!ModelState.IsValid) return View();

            if (old.Name == category.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            bool result = await _context.Categories.AnyAsync(s => s.Name.ToLower().Trim() == category.Name.ToLower().Trim() && s.Id != old.Id);
            if (result)
            {
                ModelState.AddModelError("Icon", "Movcuddur");
                return View(old);

            }
            old.Name = category.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (id == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

    }
}
