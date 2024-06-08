using Microsoft.AspNetCore.Mvc;
using OganiShoppingProject.Data;
using OganiShoppingProject.Models;
using OganiShoppingProject.ViewModel;

namespace OganiShoppingProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly ShoppingDbContext _context;
        public BlogController(ShoppingDbContext context)
        {
            _context = context;
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
        public IActionResult Details(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            News news1 = _context.Newss.FirstOrDefault(n => n.Id == id);
            if (news1 == null) return NotFound();

            List<News> news = _context.Newss.Where(n => n.Id != news1.Id).Take(3).ToList();

            BlogDetailsVM detailsVM = new BlogDetailsVM
            {
                news = news1,
                newss = news
            };

            return View(detailsVM);
        }
    }
}
