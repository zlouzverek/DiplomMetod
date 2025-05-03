using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    public class InstructionManualController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
