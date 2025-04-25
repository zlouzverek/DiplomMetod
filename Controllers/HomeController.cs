using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
