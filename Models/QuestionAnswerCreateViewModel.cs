using System.ComponentModel;
using System.Reflection;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class QuestionAnswerCreateViewModel
    {
        public QuestionAnswerCreateViewModel()
        {
            KeyWords = new List<KeyWordViewModel>();
        }

        public QuestionAnswerCreateViewModel(
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
            Event = form.Event;
            Question = form.Question;
            Answer = form.Answer;

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
                Comment = form.Explanation.Comment;
                Description = form.Explanation.Description;
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

        public QuestionAnswerCreateViewModel(
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
        public IEnumerable<SelectListItem> FormTypes { get; set; }
        public int ReferenceBooksId { get; set; }
        public IEnumerable<SelectListItem> ReferenceBooks { get; set; }
        public int RequisiteNumber { get; set; }
        public int Code { get; set; }
        public string Event { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ExplanationName { get; set; }
        public string ExplanationFullName { get; set; }
        public string? ExplanationNumber { get; set; }
        public DateTime ExplanationDate { get; set; }
        public int OrganizationId { get; set; }
        public IEnumerable<SelectListItem> Organizations { get; set; }
        public int RegionDivisionId { get; set; }
        public IEnumerable<SelectListItem> RegionDivisions { get; set; }
        public IEnumerable<KeyWordViewModel> KeyWords { get; set; }
        public string? FileLink { get; set; }
        public bool IsAgreedGenProk { get; set; }
        public ApproveLevel ApproveLevel { get; set; }
        public IEnumerable<SelectListItem> ApproveLevels { get; set; }
        public bool IsRevelant { get; set; }
        public bool IsFavorites { get; set; }
        public string? Comment { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; }


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
                Event = Event,
                Question = Question,
                Answer = Answer,
                Explanation = new Explanation
                {
                    Name = ExplanationName,
                    FullName = ExplanationFullName,
                    Date = ExplanationDate,
                    Number = ExplanationNumber,
                    IsAgreedGenProk = IsAgreedGenProk,
                    IsRevelant = IsRevelant,
                    IsFavorites = IsFavorites,
                    Comment = Comment,
                    Description = Description,
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