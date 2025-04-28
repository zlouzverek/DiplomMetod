using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class OrganizationCreateViewModel
    {

        public OrganizationCreateViewModel(IEnumerable<Organization> organization)
        {

        }

        public string Name { get; set; }
              
        public Organization ToFormEntity()
        {
            var organization = new Organization
            {
                Name = Name,
                
            };

            return organization;
        }
    }
}
