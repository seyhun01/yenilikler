using Microsoft.AspNetCore.Mvc;

namespace WareHause.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
