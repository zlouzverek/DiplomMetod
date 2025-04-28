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
        public FormCreateViewModel(IEnumerable<FormType> formTypes, IEnumerable<ReferenceBook> referenceBooks, IEnumerable<Organization> organizations)
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

            /*#FIXME:Добавил Organizations, прописал тут и в контроллере*/
            Organizations = organizations.Select(rb => new SelectListItem
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

        public string? ExplanationNumber { get; set; }

        public DateTime ExplanationDate { get; set; }

        /*#FIXME: Тут вопрос: OrganizationName заменил на ID (вроде дожен ID по base автоматически создаваться)*/
        public int OrganizationId { get; set; }

        public IEnumerable<SelectListItem> Organizations { get; set; }

        public string RegionDivisionName {  get; set; }

        public IEnumerable<KeyWordViewModel> KeyWords { get; set; }

        public string? FileLink { get; set; }

        public bool IsAgreedGenProk { get; set; }

        /*#FIXME:Не получилось с enum, может все же bool или как?*/
        //public enum ApproveLevel { get; set; }

        public bool IsRevelant { get; set; }

        public bool IsFavorites { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public IFormFile File { get; set; }

        /*#FIXME:Заменил OrganizationName в public Form ToFormEntity(), на OrganizationId*/
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
                    //ApproveLevel = ApproveLevel,
                    OrganizationId = OrganizationId,
                    /*Organizations = new OrganizationName { Name = OrganizationName }*/

                },
                /*#FIXME: Есть вопрос, т.к. это должно из списка выпадающего*/
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
