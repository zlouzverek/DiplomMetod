using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class FormController : Controller
    {

        private readonly IFormRepository _formRepository;

        public FormController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var forms = await _formRepository.GetAll();

            return View(forms);
        }

        [HttpGet]
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] FormSearchFilter queryFilter)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _formRepository.GetById(Id);

            return View(result);
        }

        /*
        [HttpPost]
        //#FIXME: Добавил метод ExportToExcel//
        public async Task<IActionResult> ExportToExcel()
        {
        
            return View();
        }

         //#FIXME: Добавил метод ExportToPdf//
         [HttpPost]
        /*public async Task<IActionResult> ExpotToPdf()
        {
            return View();
        }
        */



    }
}
