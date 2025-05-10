using DiplomMetod.Data.Entites;
using DiplomMetod.Data.Enums;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using DiplomMetod.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Controllers
{
    [Authorize]
    public class FormController : Controller
    {
        private readonly IFormRepository _formRepository;
        private readonly IReferenceBookRepository _referenceBookRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IRegionDivisionRepository _regionDivisionRepository;
        private readonly IFormTypeRepository _formTypeRepository;
        private readonly IFileService _fileService;
        private readonly IFormExportService _formExportService;

        public FormController(
            IFormRepository formRepository,
            IReferenceBookRepository referenceBookRepository,
            IOrganizationRepository organizationRepository,
            IRegionDivisionRepository regionDivisionRepository,
            IFormTypeRepository formTypeRepository,
            IFileService fileService,
            IFormExportService formExportService)
        {
            _formRepository = formRepository;
            _referenceBookRepository = referenceBookRepository;
            _organizationRepository = organizationRepository;
            _regionDivisionRepository = regionDivisionRepository;
            _formTypeRepository = formTypeRepository;
            _fileService = fileService;
            _formExportService = formExportService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(FormSearchViewModel filters)
        {
            var query = GetFormQueryFiltered(filters);

            // выполняем запрос и получаем отфильтрованные данных
            var forms = await query.ToListAsync();
            return View(forms);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var formTypes = await _formTypeRepository.GetAll();
            var referenceBooks = await _referenceBookRepository.GetAll();
            var organizations = await _organizationRepository.GetAll();
            var regionDivisions = await _regionDivisionRepository.GetAll();

            var viewModel = new FormCreateViewModel(formTypes, referenceBooks, organizations, regionDivisions);
            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        //Папка для сохранения на wwwroot uploadFiles
        [HttpPost]
        public async Task<IActionResult> Create(FormCreateViewModel formCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var form = formCreateViewModel.ToFormEntity();

                var formType = await _formTypeRepository.GetById(form.FormTypeId);

                form.FormType = formType;

                if (formCreateViewModel.File != null && formCreateViewModel.File.Length > 0)
                {
                    form.FileLink = await _fileService.SaveFile(formCreateViewModel.File , "uploadFiles");
                }

                await _formRepository.Add(form);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var form = await _formRepository.GetById(id);
            if (form != null)
                await _formRepository.Remove(form);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var form = await _formRepository.GetById(id);
            if (form == null)
            {
                return NotFound();
            }

            var formTypes = await _formTypeRepository.GetAll();
            var referenceBooks = await _referenceBookRepository.GetAll();
            var organizations = await _organizationRepository.GetAll();
            var regionDivisions = await _regionDivisionRepository.GetAll();

            var viewModel = new FormCreateViewModel(
                form,
                formTypes,
                referenceBooks,
                organizations,
                regionDivisions
            );

            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(FormCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var form = await _formRepository.GetById(viewModel.Id);

                form.Explanation.FullName = viewModel.ExplanationFullName;
                form.Explanation.Name = viewModel.ExplanationName;
                form.Explanation.Number = viewModel.ExplanationNumber;
                form.FileLink = form.FileLink;
                form.RequisiteNumber = viewModel.RequisiteNumber;
                form.FormTypeId = viewModel.FormTypeId;

                var keywords = new List<KeyWord>();
                
                foreach( var k in viewModel.KeyWords)
                {
                    keywords.Add(new KeyWord()
                    {
                        Name = k.Name
                    });
                }

                form.KeyWords = keywords;
                form.ReferenceBooksId = viewModel.ReferenceBooksId;
                form.RegionsDivisionsId = viewModel.RegionDivisionId;
				//Добавил IsQuestion
				form.IsQuestion = viewModel.IsQuestion;
				form.Question = viewModel.Question;
				form.Answer = viewModel.Answer;
				form.Event = viewModel.Event;


				if (viewModel.File != null && viewModel.File.Length > 0)
                {
                    form.FileLink = await _fileService.SaveFile(viewModel.File, "uploadFiles");
                }

                await _formRepository.Update(form);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", viewModel.Id);
            }

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

            if (!string.IsNullOrEmpty(filters.NameFormType))
                query = query.Where(f => f.FormType.Name.Contains(filters.NameFormType));

            if (filters.RequisiteNumber.HasValue)
                query = query.Where(f => f.RequisiteNumber == filters.RequisiteNumber);

            if (filters.Code.HasValue)
                query = query.Where(f => f.Code == filters.Code);

            if (!string.IsNullOrEmpty(filters.ReferenceBookName))
                query = query.Where(f => f.ReferenceBook.Name.Contains(filters.ReferenceBookName));

            if (!string.IsNullOrEmpty(filters.KeyWordName))
                query = query.Where(f => f.KeyWords.Any(kw => kw.Name.Contains(filters.KeyWordName)));

            if (!string.IsNullOrEmpty(filters.ExplanationNumber))
                query = query.Where(f => f.Explanation.Number.Contains(filters.ExplanationNumber));

            if (filters.ExplanationDate.HasValue)
                query = query.Where(f => f.Explanation.Date.Date == filters.ExplanationDate.Value.Date);

            if (!string.IsNullOrEmpty(filters.OrganizationName))
                query = query.Where(f => f.Explanation.Organization.Name.Contains(filters.OrganizationName));

			if (!string.IsNullOrEmpty(filters.RegionsDivisionName))
				query = query.Where(f => f.RegionsDivision.Name.Contains(filters.RegionsDivisionName));

			if (!string.IsNullOrEmpty(filters.Event))
				query = query.Where(f => f.Event.Contains(filters.Event));

			if (!string.IsNullOrEmpty(filters.Comment))
				query = query.Where(f => f.Explanation.Comment.Contains(filters.Comment));

			//Нюанс с ApproveLevel при организации поиска
			if (!string.IsNullOrEmpty(filters.ApproveLevel))
			{
				var level = (ApproveLevel)int.Parse(filters.ApproveLevel);
				query = query.Where(f => f.Explanation.ApproveLevel == level);
			}

			if (filters.IsAgreedGenProk.HasValue)
                query = query.Where(f => f.Explanation.IsAgreedGenProk == filters.IsAgreedGenProk.Value);

            if (filters.IsRevelant.HasValue)
                query = query.Where(f => f.Explanation.IsRevelant == filters.IsRevelant.Value);

            if (filters.IsFavorites.HasValue)
                query = query.Where(f => f.Explanation.IsFavorites == filters.IsFavorites.Value);

            if (filters.IsQuestion.HasValue)
                query = query.Where(f => f.IsQuestion == filters.IsQuestion.Value);

            return query;
        }
    }
}