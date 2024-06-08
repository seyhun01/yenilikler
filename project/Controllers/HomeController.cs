using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel.Home;

namespace OganiShoppingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoppingDbContext _context;
        public HomeController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? categoryId)
        {
            Slide slide = _context.Slides.FirstOrDefault();
            if (slide == null) return BadRequest();
            List<FavoriteProduct> favproduct = _context.FavoriteProducts.Where(p => p.CategoryId == categoryId).ToList();
            List<Category> categories = _context.Categories.ToList();
            HomeVM homeVM = new HomeVM
            {
                category = categories,
                slide = slide,
                favproduct = favproduct
            };

            return View(homeVM);
        }
        public IActionResult FavProductDetails(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            FavoriteProduct product = _context.FavoriteProducts
                .Include(p => p.Category).Include(p => p.SizeofFavoriteProducts).ThenInclude(p => p.Sizes)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            List<FavoriteProduct> products = _context.FavoriteProducts.Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(4).ToList();
            FavoriteProductDetailsVM detailsVM = new FavoriteProductDetailsVM
            {
                favoriteproduct = product,
                favoriteproducts = products
            };


            return View(detailsVM);
        }
        public IActionResult FavProductBasketDetails(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            FavoriteProduct product = _context.FavoriteProducts
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            FavoriteProductDetailsVM detailsVM = new FavoriteProductDetailsVM
            {
                favoriteproduct = product
            };
            return View(detailsVM);

        }
        [HttpPost]
        public IActionResult FavProductBasketDetails(int? id, Basket basket)
        {
            if (id == null || id < 1) return BadRequest();
            FavoriteProduct product = _context.FavoriteProducts
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            basket.Image = product.Image;
            basket.Name = product.Name;
            basket.Price = product.Price;
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
