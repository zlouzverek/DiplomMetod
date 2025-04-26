using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class RegionDivisionCreateViewModel
    {

        public RegionDivisionCreateViewModel(IEnumerable<RegionDivision> regionDivision)
        {

        }

        public string Name { get; set; }


        public RegionDivision ToFormEntity()
        {
            var regionDivision = new RegionDivision
            {
                Name = Name,
               
            };

            return regionDivision;
        }
    }
}
