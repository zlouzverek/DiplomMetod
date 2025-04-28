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

        public OrganizationController(IFormRepository formRepository,
            IOrganizationRepository organizationRepository)
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
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrganizationCreateViewModel organizationCreateViewModel)
        {
            var organization = organizationCreateViewModel.ToFormEntity();

            await _organizationRepository.Add(organization);

            return RedirectToAction("Index", "Organization");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var form = await _organizationRepository.GetById(id);

            if (form != null)
                await _organizationRepository.Remove(form);

            return RedirectToAction("Index", "Organization");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var organization = await _organizationRepository.GetById(Id);

            var editViewModel = new OrganizationCreateViewModel(organization.Id, organization.Name);

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationCreateViewModel editViewModel)
        {
            var organization = await _organizationRepository.GetById(editViewModel.Id);

            organization.Name = editViewModel.Name;

            await _organizationRepository.Update(organization);

            return RedirectToAction("Index", "Organization");
        }
    }
}
