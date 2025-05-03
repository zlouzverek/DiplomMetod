using System.ComponentModel;
using System.Reflection;
using DiplomMetod.Controllers;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class FormCreateViewModel
    {
        //Реализация всплывающего Enum через DescriptionAttribute (из Entites) //
        private static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public FormCreateViewModel()
        {
            
        }
        public FormCreateViewModel(IEnumerable<FormType> formTypes, 
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

            Organizations = organizations.Select(rb => new SelectListItem
            {
                Value = rb.Id.ToString(),
                Text = rb.Name
            }).ToList();

            RegionDivisions = regionDivisions.Select(rb => new SelectListItem
            {
                Value = rb.Id.ToString(),
                Text = rb.Name
            }).ToList();

            ApproveLevels = Enum.GetValues(typeof(ApproveLevel))
            .Cast<ApproveLevel>()
            .Select(level => new SelectListItem
        {
            Value = ((int)level).ToString(),
            Text = GetEnumDescription(level)
        }).ToList();
        }

        public int FormTypeId { get; set; }

        public IEnumerable<SelectListItem> FormTypes { get; set; }

        public int ReferenceBooksId { get; set; }

        public IEnumerable<SelectListItem> ReferenceBooks { get; set; }

        public int RequisiteNumber { get; set; }
        public int Code { get; set; }

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

        //#FIXME: Тут реализован enum//
        public ApproveLevel ApproveLevel { get; set; }

        public IEnumerable<SelectListItem> ApproveLevels { get; set; }

        public bool IsRevelant { get; set; }

        public bool IsFavorites { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public IFormFile File { get; set; }

        public Form ToFormEntity()
        {
            var form = new Form
            {
                FormTypeId = FormTypeId,
                Code = Code,
                ReferenceBooksId = ReferenceBooksId,
                RequisiteNumber = RequisiteNumber,
                FileLink = FileLink,
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

            foreach (var item in KeyWords)
            {
                form.KeyWords.Add(new KeyWord
                {
                    Name = item.Name
                });
            }

            return form;
        }
    }
}
