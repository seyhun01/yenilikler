using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models.MaintoMain;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel.AdminPanel.SaleoffProductVM;
using OganiShoppingProject.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace OganiShoppingProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SaleoffProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ShoppingDbContext _context;
        public SaleoffProductController(ShoppingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 0)
        {
            var product = await _context.SaleoffProducts

                .Include(p => p.SizeofSaleoffProducts)
                .ThenInclude(p => p.Sizes)
                .Skip(page * 5).Take(4).ToListAsync();
            PaginationVM<SaleoffProduct> pagination = new PaginationVM<SaleoffProduct>
            {
                Items = product,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.SaleoffProducts.Count() / 4)

            };
            return View(pagination);
        }
        public async Task<IActionResult> Create()
        {

            ViewBag.Sizess = await _context.Sizess.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleoffProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
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
            SaleoffProduct productt = new SaleoffProduct
            {
                Image = productVM.Image,
                Name = productVM.Name,
                Desc = productVM.Desc,
                AfterPrice = productVM.AfterPrice,
                BeforePrice = productVM.BeforePrice,
                LessPercent = productVM.LessPercent,
                InStock = productVM.InStock,


                SizeofSaleoffProducts = new List<SizeofSaleoffProduct>()
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
                SizeofSaleoffProduct product = new SizeofSaleoffProduct
                {
                    SaleoffProduct = productt,
                    SizesId = prodId
                };
                productt.SizeofSaleoffProducts.Add(product);

            }
            await _context.SaleoffProducts.AddAsync(productt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            SaleoffProduct oldproduct = await _context.SaleoffProducts.Where(p => p.Id == id)

                .Include(p => p.SizeofSaleoffProducts)
                .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync();
            if (oldproduct is null)
            {
                return NotFound();

            }
            UpdateSaleoffProductVM productVM = new UpdateSaleoffProductVM
            {
                Image = oldproduct.Image,
                Name = oldproduct.Name,
                Desc = oldproduct.Desc,
                AfterPrice = oldproduct.AfterPrice,
                BeforePrice = oldproduct.BeforePrice,
                LessPercent = oldproduct.LessPercent,
                InStock = oldproduct.InStock,


                SizeIds = oldproduct.SizeofSaleoffProducts.Select(o => o.SizesId).ToList()

            };


            ViewBag.Sizess = await _context.Sizess.ToListAsync();
            return View(productVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSaleoffProductVM producttVM)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            SaleoffProduct oldproduct = await _context.SaleoffProducts.Where(p => p.Id == id)

                .Include(p => p.SizeofSaleoffProducts)
                .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync();
            if (oldproduct is null)
            {
                return NotFound();

            }



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


            oldproduct.Name = producttVM.Name;
            oldproduct.Desc = producttVM.Desc;
            oldproduct.AfterPrice = producttVM.AfterPrice;
            oldproduct.InStock = producttVM.InStock;
            oldproduct.BeforePrice = producttVM.BeforePrice;
            oldproduct.LessPercent = producttVM.LessPercent;

            List<int> prodId = producttVM.SizeIds.Where(iid => !oldproduct.SizeofSaleoffProducts.Exists(o => o.SizesId == iid)).ToList();
            foreach (var productId in prodId)
            {
                bool results = await _context.Sizess.AnyAsync(p => p.Id == productId);
                if (!results)
                {
                    ViewBag.Sizess = await _context.Sizess.ToListAsync();
                    ModelState.AddModelError("SizeIds", " Yeniden yoxla");
                    return View();
                }
                SizeofSaleoffProduct sizeproduct = new SizeofSaleoffProduct
                {
                    SizesId = productId,
                    SaleoffProductId = oldproduct.Id

                };
                oldproduct.SizeofSaleoffProducts.Add(sizeproduct);


            }
            List<SizeofSaleoffProduct> delete = oldproduct.SizeofSaleoffProducts.Where(d => !producttVM.SizeIds.Any(i => i == d.SizesId)).ToList();
            _context.SizeofSaleoffProducts.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();

            }
            SaleoffProduct oldproduct = await _context.SaleoffProducts.Where(p => p.Id == id)

                .Include(p => p.SizeofSaleoffProducts)
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
            _context.SaleoffProducts.Remove(oldproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
