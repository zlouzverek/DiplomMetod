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

        public RegionDivisionController(IFormRepository formRepository, 
            IRegionDivisionRepository regionDivisionRepository)
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
		public async Task<IActionResult> Create()
		{
			return View();
		}
	


        [HttpPost]
        public async Task<IActionResult> Create(RegionDivisionCreateViewModel regionDivisionCreateViewModel)
        {
            var regionDivision = regionDivisionCreateViewModel.ToFormEntity();

            await _regionDivisionRepository.Add(regionDivision);


            return RedirectToAction("Index", "RegionDivision");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _regionDivisionRepository.GetById(id);

            if (form != null)
                await _regionDivisionRepository.Remove(form);

            return RedirectToAction("Index", "RegionDivision");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var regionDivision = await _regionDivisionRepository.GetById(Id);

			var editViewModel = new RegionDivisionCreateViewModel(regionDivision.Id, regionDivision.Name);

			return View(editViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(RegionDivisionCreateViewModel editViewModel)
		{
			var regionDivision = await _regionDivisionRepository.GetById(editViewModel.Id);

			regionDivision.Name = editViewModel.Name;

			await _regionDivisionRepository.Update(regionDivision);

			return RedirectToAction("Index", "RegionDivision");
		}



	}
}
