using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel.Shop;
using OganiShoppingProject.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace OganiShoppingProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShoppingDbContext _context;
        public ShopController(ShoppingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int categoryId, int page = 0)
        {

            List<Product> product = _context.Products.Where(p => p.CategoryId == categoryId).Skip(page * 5).Take(6).ToList();

            PaginationVM<Product> pagination = new PaginationVM<Product>
            {
                Items = product,
                CurrentPage = page,
                TotalPage = Math.Ceiling((decimal)_context.Products.Count() / 6)
            };
            List<SaleoffProduct> saleoffs = _context.SaleoffProducts.ToList();
            List<Category> categories = _context.Categories.ToList();

            ShopVM shopVM = new ShopVM
            {
                category = categories,
                pagination = pagination,
                saleoffproducts = saleoffs
            };

            return View(shopVM);
        }
        public IActionResult SaleoffDetails(int? id)
        {
            if (id == null) return BadRequest();
            SaleoffProduct product = _context.SaleoffProducts
               .Include(p => p.SizeofSaleoffProducts).ThenInclude(p => p.Sizes)
               .FirstOrDefault(s => s.Id == id);



            if (product == null) return NotFound();

            List<SaleoffProduct> products = _context.SaleoffProducts.Where(p => p.Id != product.Id).Take(4).ToList();

            SaleoffProductDetailsVM detailsVM = new SaleoffProductDetailsVM
            {
                saleoffproduct = product,
                saleoffproducts = products

            };
            return View(detailsVM);
        }
        public IActionResult ProductDetails(int? id)
        {
            if (id == null) return BadRequest();
            Product product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.SizeofProducts).ThenInclude(p => p.Sizes).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            List<Product> products = _context.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(4).ToList();
            ProductDetailsVM detailsVM = new ProductDetailsVM
            {
                product = product,
                products = products
            };

            return View(detailsVM);
        }
        public IActionResult BasketProductDetails(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Product product = _context.Products
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            ProductDetailsVM detailsVM = new ProductDetailsVM
            {
                product = product
            };
            return View(detailsVM);

        }
        [HttpPost]
        public IActionResult BasketProductDetails(int? id, Basket basket)
        {
            if (id == null || id < 1) return BadRequest();
            Product product = _context.Products
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            basket.Image = product.Image;
            basket.Name = product.Name;
            basket.Price = product.Price;
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }
        public IActionResult BasketSaleoffDetails(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            SaleoffProduct product = _context.SaleoffProducts
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            SaleoffProductDetailsVM detailsVM = new SaleoffProductDetailsVM
            {
                saleoffproduct = product
            };
            return View(detailsVM);

        }
        [HttpPost]
        public IActionResult BasketSaleoffDetails(int? id, Basket basket)
        {
            if (id == null || id < 1) return BadRequest();
            SaleoffProduct product = _context.SaleoffProducts
           .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            basket.Image = product.Image;
            basket.Name = product.Name;
            basket.Price = product.AfterPrice;
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }

    }
}
