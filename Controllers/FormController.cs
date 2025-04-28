using DiplomMetod.Data.Entites;
using DiplomMetod.Data.Enums;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using DiplomMetod.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Controllers
{

    public class FormController : Controller
    {

        private readonly IFormRepository _formRepository;
        private readonly IReferenceBookRepository _referenceBookRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IRegionDivisionRepository _regionDivisionRepository;
        private readonly IFormTypeRepository _formTypeRepository;
        private readonly IFileService _fileService;
        private readonly IFormExportService _formExportService;

        public FormController(IFormRepository formRepository,
            IReferenceBookRepository referenceBookRepository,
            IOrganizationRepository organizationRepository,
            IFileService fileService,
            IFormExportService formExportService)
        public FormController(IFormRepository formRepository, 
            IReferenceBookRepository referenceBookRepository, 
            IOrganizationRepository organizationRepository, 
            IRegionDivisionRepository regionDivisionRepository,
            IFormTypeRepository formTypeRepository)
        {
            _formRepository = formRepository;
            _referenceBookRepository = referenceBookRepository;
            _organizationRepository = organizationRepository;
            _fileService = fileService;
            _formExportService = formExportService;
            _regionDivisionRepository = regionDivisionRepository;
            _formTypeRepository = formTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(FormSearchViewModel filters)
        {
            var query = GetFormQueryFiltered(filters);

            // выполняем запрос и получаем отфильтрованные данных
            var forms = await query.ToListAsync();

            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var formTypes = await _formTypeRepository.GetAll();

            var referenceBook = await _referenceBookRepository.GetAll();

            var organizations = await _organizationRepository.GetAll();

            var regionDivisions = await _regionDivisionRepository.GetAll();

            var formCreateViewModel = new FormCreateViewModel(formTypes, referenceBook, organizations, regionDivisions);

            return View(formCreateViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(FormCreateViewModel formCreateViewModel)
        {

            var form = formCreateViewModel.ToFormEntity();

            if (formCreateViewModel.File != null && formCreateViewModel.File.Length > 0)
            {
                form.FileLink = await _fileService.SaveFile(formCreateViewModel.File, "uploadFiles");
            }

            await _formRepository.Add(form);

            return RedirectToAction("Index", "Form");

        }


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
           
            var formTypes = await _formTypeRepository.GetAll();

            var referenceBook = await _referenceBookRepository.GetAll();

            //var editCreateViewModel = new EditCreateViewModel(formTypes, referenceBook);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Export(FormSearchViewModel filters, string exportType)
        {
            var query = GetFormQueryFiltered(filters);

            var forms = await query.ToListAsync();

            var type = Enum.Parse<FormExportType>(exportType, true);

            var result = _formExportService.Export(forms.ToExportModel(), type);

            return result;

        }

        private IQueryable<Form> GetFormQueryFiltered(FormSearchViewModel filters)
        {
            var query = _formRepository.GetQueryAllWithIncludes();

            if (!string.IsNullOrEmpty(filters.InventoryNumber))
            {
                query = query.Where(f => f.InventoryNumber.Contains(filters.InventoryNumber));
            }

            if (!string.IsNullOrEmpty(filters.NameFormType))
            {
                query = query.Where(f => f.FormType.Name.Contains(filters.NameFormType));
            }

            if (filters.RequisiteNumber.HasValue)
            {
                query = query.Where(f => f.RequisiteNumber == filters.RequisiteNumber);
            }

            if (filters.Code.HasValue)
            {
                query = query.Where(f => f.Code == filters.Code);
            }

            if (!string.IsNullOrEmpty(filters.ReferenceBookName))
            {
                query = query.Where(f => f.ReferenceBook.Name.Contains(filters.ReferenceBookName));
            }

            if (!string.IsNullOrEmpty(filters.KeyWordName))
            {
                query = query.Where(f => f.KeyWords.Any(kw => kw.Name.Contains(filters.KeyWordName)));
            }

            if (!string.IsNullOrEmpty(filters.ExplanationNumber))
            {
                query = query.Where(f => f.Explanation.Number.Contains(filters.ExplanationNumber));
            }

            if (filters.ExplanationDate.HasValue)
            {
                query = query.Where(f => f.Explanation.Date.Date == filters.ExplanationDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filters.OrganizationName))
            {
                query = query.Where(f => f.Explanation.Organization.Name.Contains(filters.OrganizationName));
            }

            if (filters.IsAgreedGenProk.HasValue)
            {
                query = query.Where(f => f.Explanation.IsAgreedGenProk == filters.IsAgreedGenProk.Value);
            }

            if (!string.IsNullOrEmpty(filters.ApproveLevel))
            {
                query = query.Where(f => f.Explanation.ApproveLevel.ToString().Contains(filters.ApproveLevel));
            }

            if (filters.IsRevelant.HasValue)
            {
                query = query.Where(f => f.Explanation.IsRevelant == filters.IsRevelant.Value);
            }

            if (!string.IsNullOrEmpty(filters.RegionsDivisionName))
            {
                query = query.Where(f => f.RegionsDivision.Name.Contains(filters.RegionsDivisionName));
            }

            if (!string.IsNullOrEmpty(filters.Comment))
            {
                query = query.Where(f => f.Explanation.Comment.Contains(filters.Comment));
            }

            if (filters.IsFavorites.HasValue)
            {
                query = query.Where(f => f.Explanation.IsFavorites == filters.IsFavorites.Value);
            }

            return query;
        }

    }
}
