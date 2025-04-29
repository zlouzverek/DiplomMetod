using System.ComponentModel;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomMetod.Models
{
    public class RegionDivisionCreateViewModel
	{
		public RegionDivisionCreateViewModel()
		{

		}

		public RegionDivisionCreateViewModel(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public string Name { get; set; }

		public int Id { get; set; }

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
