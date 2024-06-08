using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models.MaintoMain;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel.AdminPanel.FavoriteProductVM;
using OganiShoppingProject.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FavoriteProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ShoppingDbContext _context;
        public FavoriteProductController(ShoppingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 0)
        {
            var product = await _context.FavoriteProducts
                .Include(p => p.Category)
                .Include(p => p.SizeofFavoriteProducts)
                .ThenInclude(p => p.Sizes)
                .Skip(page * 5).Take(5).ToListAsync();
            PaginationVM<FavoriteProduct> pagination = new PaginationVM<FavoriteProduct>
            {

                Items = product,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.FavoriteProducts.Count() / 4)

            };

            return View(pagination);
        }
        public async Task<IActionResult> Create()
        {

            ViewBag.Sizess = await _context.Sizess.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFavoriteProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Categories.AnyAsync(s => s.Id == productVM.CategoryId);
            if (!result)
            {
                ModelState.AddModelError("CategoryId", "uygun deyil");
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }

            if (productVM == null)

            {
                ModelState.AddModelError("Photo", "Sekil Secilmeyib");
                return View();
            }

            if (!productVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Tipi sehvdir");
                return View();
            }



            if (productVM.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu Odemir");
                return View();
            }

            var filename = Guid.NewGuid().ToString() + "" + productVM.Photo.FileName;
            productVM.Image = filename;
            string path = Path.Combine(_env.WebRootPath, "Assets/img", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await productVM.Photo.CopyToAsync(stream);
            }
            FavoriteProduct productt = new FavoriteProduct
            {
                Image = productVM.Image,
                Name = productVM.Name,
                Decs = productVM.Desc,
                Price = productVM.Price,
                InStock = productVM.InStock,
                CategoryId = productVM.CategoryId,
                SizeofFavoriteProducts = new List<SizeofFavoriteProduct>()
            };


            if (productVM.SizeIds is null)
            {

                ViewBag.Sizess = await _context.Sizess.ToListAsync();
                ModelState.AddModelError("SizeIds", "Movcud deyil");
                return View();
            }

            foreach (var prodId in productVM.SizeIds)
            {
                bool results = await _context.Sizess.AnyAsync(c => c.Id == prodId);
                if (!results)
                {
                    ViewBag.Sizess = await _context.Sizess.ToListAsync();
                    ModelState.AddModelError("SizeIds", " deyil");
                    return View();
                }
                SizeofFavoriteProduct product = new SizeofFavoriteProduct
                {
                    FavoriteProduct = productt,
                    SizesId = prodId
                };
                productt.SizeofFavoriteProducts.Add(product);

            }
            await _context.FavoriteProducts.AddAsync(productt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            FavoriteProduct oldproduct = await _context.FavoriteProducts.Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.SizeofFavoriteProducts)
                .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync();
            if (oldproduct is null)
            {
                return NotFound();

            }
            UpdateFavoriteProductVM productVM = new UpdateFavoriteProductVM
            {
                Image = oldproduct.Image,
                Name = oldproduct.Name,
                Desc = oldproduct.Decs,
                Price = oldproduct.Price,
                InStock = oldproduct.InStock,

                CategoryId = oldproduct.CategoryId,
                SizeIds = oldproduct.SizeofFavoriteProducts.Select(o => o.SizesId).ToList()

            };


            ViewBag.Sizess = await _context.Sizess.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(productVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateFavoriteProductVM producttVM)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            FavoriteProduct oldproduct = await _context.FavoriteProducts.Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.SizeofFavoriteProducts)
                .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync();
            if (oldproduct is null)
            {
                return NotFound();

            }
            FavoriteProduct exits = await _context.FavoriteProducts.FirstOrDefaultAsync(e => e.Id == id);
            if (exits == null) return NotFound();

            bool result = await _context.Categories.AnyAsync(s => s.Id == producttVM.CategoryId);
            if (!result)
            {
                ModelState.AddModelError("ProductId", "Uygun deyil");
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }

            exits.CategoryId = producttVM.CategoryId;
            if (producttVM == null)
            {
                ModelState.AddModelError("Photo", "Sekil secilmeyib");
                return View();
            }
            if (!producttVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Sekil tipi duzgun deyil");
                return View();
            }
            if (producttVM.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Olcu duzgun deyil");
                return View();
            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + oldproduct.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var fileName = Guid.NewGuid().ToString() + "" + producttVM.Photo.FileName;
            oldproduct.Image = fileName;

            string newpath = Path.Combine(_env.WebRootPath, "Assets/img", fileName);
            using (FileStream stream = new FileStream(newpath, FileMode.Create))
            {
                await producttVM.Photo.CopyToAsync(stream);
            }
            ViewBag.Sizess = await _context.Sizess.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            oldproduct.Name = producttVM.Name;
            oldproduct.Decs = producttVM.Desc;
            oldproduct.Price = producttVM.Price;
            oldproduct.InStock = producttVM.InStock;

            List<int> prodId = producttVM.SizeIds.Where(iid => !oldproduct.SizeofFavoriteProducts.Exists(o => o.SizesId == iid)).ToList();
            foreach (var productId in prodId)
            {
                bool results = await _context.Sizess.AnyAsync(p => p.Id == productId);
                if (!results)
                {
                    ViewBag.Sizess = await _context.Sizess.ToListAsync();
                    ModelState.AddModelError("SizeIds", " Yeniden yoxla");
                    return View();
                }
                SizeofFavoriteProduct sizeproduct = new SizeofFavoriteProduct
                {
                    SizesId = productId,
                    FavoriteProductId = oldproduct.Id
                };
                oldproduct.SizeofFavoriteProducts.Add(sizeproduct);


            }
            List<SizeofFavoriteProduct> delete = oldproduct.SizeofFavoriteProducts.Where(d => !producttVM.SizeIds.Any(i => i == d.SizesId)).ToList();
            _context.SizeofFavoriteProducts.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            FavoriteProduct oldproduct = await _context.FavoriteProducts.Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.SizeofFavoriteProducts)
                .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync();
            if (oldproduct is null)
            {
                return NotFound();

            }
            string path = Path.Combine(_env.WebRootPath + "Assets/img" + oldproduct.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.FavoriteProducts.Remove(oldproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
