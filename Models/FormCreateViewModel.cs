using System.ComponentModel;
using System.Reflection;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class FormCreateViewModel
    {
        public FormCreateViewModel()
        {
            KeyWords = new List<KeyWordViewModel>();
        }

        public FormCreateViewModel(
            Form form,
            IEnumerable<FormType> formTypes,
            IEnumerable<ReferenceBook> referenceBooks,
            IEnumerable<Organization> organizations,
            IEnumerable<RegionDivision> regionDivisions)
        {
            Id = form.Id;
            FormTypeId = form.FormTypeId;
            ReferenceBooksId = form.ReferenceBooksId;
            RequisiteNumber = (int)form.RequisiteNumber;
			Code = (int)form.Code;
			FileLink = form.FileLink;
            RegionDivisionId = form.RegionsDivisionsId;
			//Добавил IsQuestion (не было)
			IsQuestion = form.IsQuestion;
			Question = form.Question;
			Answer = form.Answer;
			Event = form.Event;

			if (form.Explanation != null)
            {
                ExplanationName = form.Explanation.Name;
                ExplanationFullName = form.Explanation.FullName;
                ExplanationNumber = form.Explanation.Number;
                ExplanationDate = form.Explanation.Date;
                OrganizationId = form.Explanation.OrganizationId;
                IsAgreedGenProk = form.Explanation.IsAgreedGenProk;
                ApproveLevel = form.Explanation.ApproveLevel;
                IsRevelant = form.Explanation.IsRevelant;
                IsFavorites = form.Explanation.IsFavorites;
                ExplanationComment = form.Explanation.Comment;
                ExplanationDescription = form.Explanation.Description;
                ExplanationDescription = form.Explanation.Description;
            }

            FormTypes = formTypes.Select(ft => new SelectListItem
            {
                Value = ft.Id.ToString(),
                Text = ft.Name,
                Selected = ft.Id == form.FormTypeId
            }).ToList();

            ReferenceBooks = referenceBooks.Select(rb => new SelectListItem
            {
                Value = rb.Id.ToString(),
                Text = rb.Name,
                Selected = rb.Id == form.ReferenceBooksId
            }).ToList();

            Organizations = organizations.Select(org => new SelectListItem
            {
                Value = org.Id.ToString(),
                Text = org.Name,
                Selected = org.Id == OrganizationId
            }).ToList();

            RegionDivisions = regionDivisions.Select(rd => new SelectListItem
            {
                Value = rd.Id.ToString(),
                Text = rd.Name,
                Selected = rd.Id == RegionDivisionId
            }).ToList();

            ApproveLevels = Enum.GetValues(typeof(ApproveLevel))
                .Cast<ApproveLevel>()
                .Select(level => new SelectListItem
                {
                    Value = ((int)level).ToString(),
                    Text = GetEnumDescription(level),
                    Selected = level == ApproveLevel
                }).ToList();

            KeyWords = form.KeyWords?.Select(kw => new KeyWordViewModel
            {
                Name = kw.Name
            }).ToList() ?? new List<KeyWordViewModel>();
        }

        public FormCreateViewModel(
            IEnumerable<FormType> formTypes,
            IEnumerable<ReferenceBook> referenceBooks,
            IEnumerable<Organization> organizations,
            IEnumerable<RegionDivision> regionDivisions)
        {
            FormTypes = formTypes.Select(ft => new SelectListItem
            {
                Value = ft.Id.ToString(),
                Text = ft.Name
            }).ToList();

            ReferenceBooks = referenceBooks.Select(rb => new SelectListItem
            {
                Value = rb.Id.ToString(),
                Text = rb.Name
            }).ToList();

            Organizations = organizations.Select(org => new SelectListItem
            {
                Value = org.Id.ToString(),
                Text = org.Name
            }).ToList();

            RegionDivisions = regionDivisions.Select(rd => new SelectListItem
            {
                Value = rd.Id.ToString(),
                Text = rd.Name
            }).ToList();

            ApproveLevels = Enum.GetValues(typeof(ApproveLevel))
                .Cast<ApproveLevel>()
                .Select(level => new SelectListItem
                {
                    Value = ((int)level).ToString(),
                    Text = GetEnumDescription(level)
                }).ToList();

            KeyWords = new List<KeyWordViewModel>();
        }

        //Тут реализован Enum по ExplanationDescription
        private static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        // Модель для создания
        public int Id { get; set; }

        public int FormTypeId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> FormTypes { get; set; }

        public int ReferenceBooksId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ReferenceBooks { get; set; }

        public int RequisiteNumber { get; set; }

        public int Code { get; set; }
        public string ExplanationName { get; set; }

        public string ExplanationFullName { get; set; }

        public string? ExplanationNumber { get; set; }

        public DateTime ExplanationDate { get; set; }

        public int OrganizationId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Organizations { get; set; }

        public int RegionDivisionId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RegionDivisions { get; set; }

        [ValidateNever]
        public IEnumerable<KeyWordViewModel> KeyWords { get; set; }

        public string? FileLink { get; set; }

        public bool IsAgreedGenProk { get; set; }

        public ApproveLevel ApproveLevel { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApproveLevels { get; set; }

        public bool IsRevelant { get; set; }

        public bool IsFavorites { get; set; }

        public string? ExplanationComment { get; set; }

        public string? ExplanationDescription { get; set; }

        public IFormFile? File { get; set; }

        public bool IsQuestion { get; set; }

		public string? Question { get; set; }

		public string? Answer { get; set; }

		public string? Event { get; set; }


		public Form ToFormEntity()
        {
            var form = new Form
            {
                Id = Id,
                FormTypeId = FormTypeId,
                Code = Code,
                ReferenceBooksId = ReferenceBooksId,
                RequisiteNumber = RequisiteNumber,
                FileLink = FileLink,
				//Добавил сюда IsQuestion (не было)
				IsQuestion = IsQuestion,
				Question = Question,
                Answer = Answer,
                Event = Event,

				Explanation = new Explanation
                {
                    Name = ExplanationName,
                    FullName = ExplanationFullName,
                    Date = ExplanationDate,
                    Number = ExplanationNumber,
                    IsAgreedGenProk = IsAgreedGenProk,
                    IsRevelant = IsRevelant,
                    IsFavorites = IsFavorites,
                    Comment = ExplanationComment,
                    Description = ExplanationDescription,
                    ApproveLevel = ApproveLevel,
                    OrganizationId = OrganizationId,
                },
                RegionsDivisionsId = RegionDivisionId,
            };

            if (KeyWords != null)
            {
                foreach (var item in KeyWords)
                {
                    form.KeyWords.Add(new KeyWord
                    {
                        Name = item.Name
                    });
                }
            }

            return form;
        }
    }
}