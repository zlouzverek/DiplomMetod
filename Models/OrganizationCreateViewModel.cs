using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class OrganizationCreateViewModel
    {
        public OrganizationCreateViewModel()
        {
            
        }

        public OrganizationCreateViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Name { get; set; }

        public int Id { get; set; }
              
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
