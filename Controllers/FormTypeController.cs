using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FormTypeController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IFormTypeRepository _formTypeRepository;

        public FormTypeController(IFormRepository formRepository, 
            IFormTypeRepository formTypeRepository)
        {
            _formRepository = formRepository;
            _formTypeRepository = formTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formType = await _formTypeRepository.GetAll();

            return View(formType);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(FormTypeCreateViewModel formTypeCreateViewModel)
        {
            var formType = formTypeCreateViewModel.ToFormEntity();

            await _formTypeRepository.Add(formType);

            return RedirectToAction("Index", "FormType");
        }

		public async Task<IActionResult> Delete(int id)
		{
			var form = await _formTypeRepository.GetById(id);

			if (form != null)
				await _formTypeRepository.Remove(form);

			return RedirectToAction("Index", "Organization");
		}


		[HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var formType = await _formTypeRepository.GetById(Id);

            var editViewModel = new FormTypeCreateViewModel(formType.Id, formType.Name, formType.FullName);

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FormTypeCreateViewModel editViewModel)
        {
            var formType = await _formTypeRepository.GetById(editViewModel.Id);

            formType.Name = editViewModel.Name;

            formType.FullName = editViewModel.FullName;

            await _formTypeRepository.Update(formType);

            return RedirectToAction("Index", "FormType");
        }

    }
}
