using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        private readonly ShoppingDbContext _context;
        private readonly IWebHostEnvironment _env;
        public NewsController(ShoppingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 0)
        {
            List<News> news = _context.Newss.Skip(page * 5).Take(4).ToList();
            PaginationVM<News> pagination = new PaginationVM<News>
            {
                Items = news,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Newss.Count() / 4)

            };

            return View(pagination);
        }
        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(News news)
        {

            if (ModelState.IsValid) return View();

            if (news == null)

            {
                ModelState.AddModelError("Photo", "Sekil Secilmeyib");
                return View();



            }

            if (!news.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Tipi sehvdir");
                return View();

            }



            if (news.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu Odemir");
                return View();
            }

            var filename = Guid.NewGuid().ToString() + "" + news.Photo.FileName;
            news.Image = filename;
            string path = Path.Combine(_env.WebRootPath, "Assets/img", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await news.Photo.CopyToAsync(stream);
            }
            await _context.Newss.AddAsync(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            News news = await _context.Newss.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }





        [HttpPost]
        public async Task<IActionResult> Update(int? id, News news)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }


            News old = await _context.Newss.FirstOrDefaultAsync(o => o.Id == id);
            if (old == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid) return View();

            if (news == null)
            {
                ModelState.AddModelError("Photo", "Sekil secilmeyib");
                return View();
            }
            if (!news.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Sekil tipi duzgun deyil");
                return View();
            }
            if (news.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu duzgun deyil");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + old.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var fileName = Guid.NewGuid().ToString() + "" + news.Photo.FileName;
            old.Image = fileName;

            string newpath = Path.Combine(_env.WebRootPath, "Assets/img", fileName);
            using (FileStream stream = new FileStream(newpath, FileMode.Create))
            {
                await news.Photo.CopyToAsync(stream);
            }
            old.Desc = news.Desc;
            old.Uptitle = news.Uptitle;
            old.Title = news.Title;
            old.Time = news.Time;
            old.Desc = news.Desc;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            News news = await _context.Newss.FirstOrDefaultAsync(n => n.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + news.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Newss.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
