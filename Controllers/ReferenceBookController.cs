using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class ReferenceBookController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IReferenceBookRepository _referenceBookRepository;

        public ReferenceBookController(IFormRepository formRepository, IReferenceBookRepository referenceBookRepository)
        {
            _formRepository = formRepository;
            _referenceBookRepository = referenceBookRepository;
           
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var referenceBook = await _referenceBookRepository.GetAll();

            return View(referenceBook);
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
            var referenceBook = await _referenceBookRepository.GetAll();
 
            var referenceBookCreateViewModel = new ReferenceBookCreateViewModel(referenceBook);

            return View(referenceBookCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ReferenceBookCreateViewModel referenceBookCreateViewModel)
        {
            var referenceBook = referenceBookCreateViewModel.ToFormEntity();

            await _referenceBookRepository.Add(referenceBook);


            return RedirectToAction("Index", "Form");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _referenceBookRepository.GetById(id);

            if (form != null)
                await _referenceBookRepository.Remove(form);

            return RedirectToAction("Index", "Form");
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

  
    }
}
