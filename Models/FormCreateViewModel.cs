using DiplomMetod.Data.Entites;

namespace DiplomMetod.Models
{
    public class FormCreateViewModel
    {
       
        public FormCreateViewModel(IEnumerable<FormType> formTypes, IEnumerable<ReferenceBook> referenceBooks)
        {
            FormTypes = formTypes;
            ReferenceBooks = referenceBooks;
        }

        public IEnumerable<FormType> FormTypes { get; set; }

        public IEnumerable<ReferenceBook> ReferenceBooks { get; set; }

    }
}
