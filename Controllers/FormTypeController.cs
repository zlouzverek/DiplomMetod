using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class FormTypeController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IFormTypeRepository _formTypeRepository;



        public FormTypeController(IFormRepository formRepository, IFormTypeRepository formTypeRepository)
        {
            _formRepository = formRepository;
            _formTypeRepository = formTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formTypes = await _formRepository.GetAll();

            return View(formTypes);
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
            var formTypes = await _formTypeRepository.GetAll();

            var formTypeCreateViewModel = new FormTypeCreateViewModel(formTypes);

            return View(formTypeCreateViewModel);
        }


        /*#FIXME: выводит ошибку. Пока не понял почему. Может создать отдельный репзиторий для FormType?
        [HttpPost]
        public async Task<IActionResult> Create(FormTypeCreateViewModel formTypeCreateViewModel)
        {
            var formTypes = formTypeCreateViewModel.ToFormEntity();

            await _formRepository.Add(formTypes);


            return RedirectToAction("Index", "Form");
        }
        */

        public async Task<IActionResult> Delete(int id)
        {
            var form = await _formRepository.GetById(id);

            if (form != null)
                await _formRepository.Remove(form);

            return RedirectToAction("Index", "Form");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var form = await _formRepository.GetById(Id);

            //var editCreateViewModel = new EditCreateViewModel(formTypes, referenceBook);
            return View(form);
        }

    }
}
