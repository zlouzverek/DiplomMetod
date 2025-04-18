using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class FormCreateViewModel
    {
        public FormCreateViewModel()
        {
            
        }
        public FormCreateViewModel(IEnumerable<FormType> formTypes, IEnumerable<ReferenceBook> referenceBooks)
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
        }

        public int FormTypeId { get; set; }

        public IEnumerable<SelectListItem> FormTypes { get; set; }

        public int ReferenceBooksId { get; set; }

        public IEnumerable<SelectListItem> ReferenceBooks { get; set; }

        public int RequisiteNumber { get; set; }
        public int Code { get; set; }

        public string ExplanationName { get; set; }

        public string ExplanationFullName { get; set; }

        public string ExplanationNumber { get; set; }

        public DateTime ExplanationDate { get; set; }

        public string OrganizationName { get; set; }

        public string RegionDivisionName {  get; set; }

        public IEnumerable<KeyWordViewModel> KeyWords { get; set; }

        public Form ToFormEntity()
        {
            var form = new Form
            {
                FormTypeId = FormTypeId,
                Code = Code,
                ReferenceBooksId = ReferenceBooksId,
                RequisiteNumber = RequisiteNumber,
                Explanation = new Explanation
                {

                    Name = ExplanationName,
                    FullName = ExplanationFullName,
                    Date = ExplanationDate,
                    Number = ExplanationNumber,
                    Organization = new Organization { Name = OrganizationName },


                },
                RegionsDivision = new RegionDivision { Name = RegionDivisionName },
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
