using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPAnel")]
    public class SlideController : Controller
    {
        private readonly ShoppingDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SlideController(ShoppingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Slide> slide = _context.Slides.ToList();

            return View(slide);
        }
        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {

            if (ModelState.IsValid) return View();

            if (slide == null)

            {
                ModelState.AddModelError("Photo", "Sekil Secilmeyib");
                return View();



            }

            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Tipi sehvdir");
                return View();

            }



            if (slide.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu Odemir");
                return View();
            }

            var filename = Guid.NewGuid().ToString() + "" + slide.Photo.FileName;
            slide.Image = filename;
            string path = Path.Combine(_env.WebRootPath, "Assets/img", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await slide.Photo.CopyToAsync(stream);
            }
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }





        [HttpPost]
        public async Task<IActionResult> Update(Slide slide)
        {
            if (ModelState.IsValid) return View();

            Slide old = await _context.Slides.FirstOrDefaultAsync(o => o.Id == slide.Id);
            if (old == null)
            {
                return NotFound();
            }


            if (slide == null)
            {
                ModelState.AddModelError("Photo", "Sekil secilmeyib");
                return View();
            }
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Sekil tipi duzgun deyil");
                return View();
            }
            if (slide.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu duzgun deyil");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + old.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var fileName = Guid.NewGuid().ToString() + "" + slide.Photo.FileName;
            old.Image = fileName;

            string newpath = Path.Combine(_env.WebRootPath, "Assets/img", fileName);
            using (FileStream stream = new FileStream(newpath, FileMode.Create))
            {
                await slide.Photo.CopyToAsync(stream);
            }
            old.Uptitle = slide.Uptitle;
            old.Title = slide.Title;
            old.Desc = slide.Desc;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Slide slide = await _context.Slides.FirstOrDefaultAsync(n => n.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + slide.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
