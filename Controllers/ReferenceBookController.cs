using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ReferenceBookController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IReferenceBookRepository _referenceBookRepository;

        public ReferenceBookController(IFormRepository formRepository, 
            IReferenceBookRepository referenceBookRepository)
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
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ReferenceBookCreateViewModel referenceBookCreateViewModel)
        {
            var referenceBook = referenceBookCreateViewModel.ToFormEntity();

            await _referenceBookRepository.Add(referenceBook);

            return RedirectToAction("Index", "ReferenceBook");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _referenceBookRepository.GetById(id);

            if (form != null)
                await _referenceBookRepository.Remove(form);

            return RedirectToAction("Index", "ReferenceBook");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var referenceBook = await _referenceBookRepository.GetById(Id);

            var editViewModel = new ReferenceBookCreateViewModel
                (referenceBook.Id, referenceBook.Name, referenceBook.FullName);

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReferenceBookCreateViewModel editViewModel)
        {
            var referenceBook = await _referenceBookRepository.GetById(editViewModel.Id);

            referenceBook.Name = editViewModel.Name;

            referenceBook.FullName = editViewModel.FullName;

            await _referenceBookRepository.Update(referenceBook);

            return RedirectToAction("Index", "ReferenceBook");
        }

    }
}
