using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class ReferenceBookCreateViewModel
    {

        public ReferenceBookCreateViewModel(IEnumerable<ReferenceBook> referenceBook)
        {

        }

        public string Name { get; set; }

        public string FullName { get; set; }

       
        public ReferenceBook ToFormEntity()
        {
            var referenceBook = new ReferenceBook
            {
                Name = Name,
                FullName = FullName,

            };

            return referenceBook;
        }
    }
}
