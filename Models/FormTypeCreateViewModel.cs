using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class FormTypeCreateViewModel
    {

        public FormTypeCreateViewModel(IEnumerable<FormType> formType)
        {

        }

        public string Name { get; set; }

        public string FullName { get; set; }


        public FormType ToFormEntity()
        {
            var formType = new FormType
            {
                Name = Name,
                FullName = FullName,

            };

            return formType;
        }
    }
}
