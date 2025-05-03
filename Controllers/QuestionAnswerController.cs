using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using DiplomMetod.Repositories;
using DiplomMetod.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace DiplomMetod.Controllers
{
	public class QuestionAnswerController : Controller
	{
		private readonly IFormRepository _formRepository;
		private readonly IReferenceBookRepository _referenceBookRepository;
		private readonly IOrganizationRepository _organizationRepository;
		private readonly IRegionDivisionRepository _regionDivisionRepository;
		private readonly IFormTypeRepository _formTypeRepository;
		private readonly IFileService _fileService;

		public QuestionAnswerController(
			IFormRepository formRepository,
			IReferenceBookRepository referenceBookRepository,
			IOrganizationRepository organizationRepository,
			IRegionDivisionRepository regionDivisionRepository,
			IFormTypeRepository formTypeRepository,
			IFileService fileService)
		{
			_formRepository = formRepository;
			_referenceBookRepository = referenceBookRepository;
			_organizationRepository = organizationRepository;
			_regionDivisionRepository = regionDivisionRepository;
			_formTypeRepository = formTypeRepository;
			_fileService = fileService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(QuestionAnswerSearchViewModel filters)
		{
			var query = GetFormQueryFiltered(filters);
			var forms = await query.ToListAsync();
			return View(forms);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var formTypes = await _formTypeRepository.GetAll();
			var referenceBooks = await _referenceBookRepository.GetAll();
			var organizations = await _organizationRepository.GetAll();
			var regionDivisions = await _regionDivisionRepository.GetAll();

			var viewModel = new QuestionAnswerCreateViewModel(formTypes, referenceBooks, organizations, regionDivisions);
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(QuestionAnswerCreateViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var form = viewModel.ToFormEntity();

				if (viewModel.File != null && viewModel.File.Length > 0)
				{
					form.FileLink = await _fileService.SaveFile(viewModel.File, "uploadFiles");
				}

				await _formRepository.Add(form);
				return RedirectToAction("Index");
			}

			// Reload select lists if model is invalid
			viewModel.FormTypes = (await _formTypeRepository.GetAll()).Select(ft => new SelectListItem
			{
				Value = ft.Id.ToString(),
				Text = ft.Name
			}).ToList();

			viewModel.ReferenceBooks = (await _referenceBookRepository.GetAll()).Select(rb => new SelectListItem
			{
				Value = rb.Id.ToString(),
				Text = rb.Name
			}).ToList();

			viewModel.Organizations = (await _organizationRepository.GetAll()).Select(org => new SelectListItem
			{
				Value = org.Id.ToString(),
				Text = org.Name
			}).ToList();

			viewModel.RegionDivisions = (await _regionDivisionRepository.GetAll()).Select(rd => new SelectListItem
			{
				Value = rd.Id.ToString(),
				Text = rd.Name
			}).ToList();

			viewModel.ApproveLevels = Enum.GetValues(typeof(ApproveLevel))
				.Cast<ApproveLevel>()
				.Select(level => new SelectListItem
				{
					Value = ((int)level).ToString(),
					Text = GetEnumDescription(level)
				}).ToList();

			return View(viewModel);
		}

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

			var viewModel = new QuestionAnswerCreateViewModel(
				form,
				formTypes,
				referenceBooks,
				organizations,
				regionDivisions
			);

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(QuestionAnswerCreateViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var form = viewModel.ToFormEntity();

				if (viewModel.File != null && viewModel.File.Length > 0)
				{
					form.FileLink = await _fileService.SaveFile(viewModel.File, "uploadFiles");
				}

				await _formRepository.Update(form);
				return RedirectToAction("Index");
			}

			// Reload select lists if model is invalid
			viewModel.FormTypes = (await _formTypeRepository.GetAll()).Select(ft => new SelectListItem
			{
				Value = ft.Id.ToString(),
				Text = ft.Name
			}).ToList();

			viewModel.ReferenceBooks = (await _referenceBookRepository.GetAll()).Select(rb => new SelectListItem
			{
				Value = rb.Id.ToString(),
				Text = rb.Name
			}).ToList();

			viewModel.Organizations = (await _organizationRepository.GetAll()).Select(org => new SelectListItem
			{
				Value = org.Id.ToString(),
				Text = org.Name
			}).ToList();

			viewModel.RegionDivisions = (await _regionDivisionRepository.GetAll()).Select(rd => new SelectListItem
			{
				Value = rd.Id.ToString(),
				Text = rd.Name
			}).ToList();

			viewModel.ApproveLevels = Enum.GetValues(typeof(ApproveLevel))
				.Cast<ApproveLevel>()
				.Select(level => new SelectListItem
				{
					Value = ((int)level).ToString(),
					Text = GetEnumDescription(level)
				}).ToList();

			return View(viewModel);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var form = await _formRepository.GetById(id);
			if (form != null)
				await _formRepository.Remove(form);

			return RedirectToAction("Index");
		}

		private IQueryable<Form> GetFormQueryFiltered(QuestionAnswerSearchViewModel filters)
		{
			var query = _formRepository.GetQueryAllWithIncludes();

			if (!string.IsNullOrEmpty(filters.InventoryNumber))
				query = query.Where(f => f.InventoryNumber.Contains(filters.InventoryNumber));

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

			if (!string.IsNullOrEmpty(filters.Event))
				query = query.Where(f => f.Event.Contains(filters.Event));

			if (!string.IsNullOrEmpty(filters.ExplanationNumber))
				query = query.Where(f => f.Explanation.Number.Contains(filters.ExplanationNumber));

			if (filters.ExplanationDate.HasValue)
				query = query.Where(f => f.Explanation.Date.Date == filters.ExplanationDate.Value.Date);

			if (!string.IsNullOrEmpty(filters.OrganizationName))
				query = query.Where(f => f.Explanation.Organization.Name.Contains(filters.OrganizationName));

			if (filters.IsAgreedGenProk.HasValue)
				query = query.Where(f => f.Explanation.IsAgreedGenProk == filters.IsAgreedGenProk.Value);

			if (!string.IsNullOrEmpty(filters.ApproveLevel))
			{
				var level = (ApproveLevel)int.Parse(filters.ApproveLevel);
				query = query.Where(f => f.Explanation.ApproveLevel == level);
			}

			if (filters.IsRevelant.HasValue)
				query = query.Where(f => f.Explanation.IsRevelant == filters.IsRevelant.Value);

			if (!string.IsNullOrEmpty(filters.RegionsDivisionName))
				query = query.Where(f => f.RegionsDivision.Name.Contains(filters.RegionsDivisionName));

			if (!string.IsNullOrEmpty(filters.Comment))
				query = query.Where(f => f.Explanation.Comment.Contains(filters.Comment));

			if (filters.IsFavorites.HasValue)
				query = query.Where(f => f.Explanation.IsFavorites == filters.IsFavorites.Value);

			return query;
		}

		private static string GetEnumDescription(Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attribute = field.GetCustomAttribute<DescriptionAttribute>();
			return attribute == null ? value.ToString() : attribute.Description;
		}
	}
}