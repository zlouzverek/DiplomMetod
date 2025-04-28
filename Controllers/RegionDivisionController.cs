using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class RegionDivisionController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IRegionDivisionRepository _regionDivisionRepository;

        public RegionDivisionController(IFormRepository formRepository, IRegionDivisionRepository regionDivisionRepository)
        {
            _formRepository = formRepository;
            _regionDivisionRepository = regionDivisionRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var regionDivision = await _regionDivisionRepository.GetAll();

            return View(regionDivision);
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
            var regionDivision = await _regionDivisionRepository.GetAll();

            var regionDivisionCreateViewModel = new RegionDivisionCreateViewModel(regionDivision);

            return View(regionDivisionCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RegionDivisionCreateViewModel regionDivisionCreateViewModel)
        {
            var regionDivision = regionDivisionCreateViewModel.ToFormEntity();

            await _regionDivisionRepository.Add(regionDivision);


            return RedirectToAction("Index", "Form");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _regionDivisionRepository.GetById(id);

            if (form != null)
                await _regionDivisionRepository.Remove(form);

            return RedirectToAction("Index", "Form");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var form = await _formRepository.GetById(Id);


            var referenceBook = await _regionDivisionRepository.GetAll();

            //var editCreateViewModel = new EditCreateViewModel(formTypes, referenceBook);
            return View(form);
        }


    }
}
