using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class FormController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IReferenceBookRepository _referenceBookRepository;

        public FormController(IFormRepository formRepository, IReferenceBookRepository referenceBookRepository)
        {
            _formRepository = formRepository;
            _referenceBookRepository = referenceBookRepository;
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
            var formTypes = await _formRepository.GetFormTypes();

            var referenceBook = await _referenceBookRepository.GetAll();

            var formCreateViewModel = new FormCreateViewModel(formTypes, referenceBook);

            return View(formCreateViewModel);


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
            var form = await _formRepository.GetById(Id);
           
            var formTypes = await _formRepository.GetFormTypes();
            var referenceBook = await _referenceBookRepository.GetAll();

            //var editCreateViewModel = new EditCreateViewModel(formTypes, referenceBook);
            return View(form);
        }

       
        [HttpPost]
        //#FIXME: Добавил метод ExportToExcel//
        public async Task<IActionResult> ExportToExcel()
        {
        
            return Ok();
        }

         //#FIXME: Добавил метод ExportToPdf//
         [HttpPost]
        public async Task<IActionResult> ExpotToPdf()
        {
            return Ok();
        }
       



    }
}
