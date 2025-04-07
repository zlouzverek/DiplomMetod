using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class FormController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
