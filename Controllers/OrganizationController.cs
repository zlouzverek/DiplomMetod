using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiplomMetod.Controllers
{

    public class OrganizationController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IFormRepository formRepository, IOrganizationRepository organizationRepository)
        {
            _formRepository = formRepository;
            _organizationRepository = organizationRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var organization = await _organizationRepository.GetAll();

            return View(organization);
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
            var organization = await _organizationRepository.GetAll();

            var organizationCreateViewModel = new OrganizationCreateViewModel(organization);

            return View(organizationCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrganizationCreateViewModel organizationCreateViewModel)
        {
            var organization = organizationCreateViewModel.ToFormEntity();

            await _organizationRepository.Add(organization);


            return RedirectToAction("Index", "Form");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _organizationRepository.GetById(id);

            if (form != null)
                await _organizationRepository.Remove(form);

            return RedirectToAction("Index", "Form");
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var form = await _formRepository.GetById(Id);

            var formTypes = await _formRepository.GetFormTypes();
            var organization = await _organizationRepository.GetAll();

            //var editCreateViewModel = new EditCreateViewModel(formTypes, referenceBook);
            return View(form);
        }


    }
}
