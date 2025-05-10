using DiplomMetod.Data.Entites;

namespace DiplomMetod.Models
{
    public class FormSearchViewModel
    {
        public string? NameFormType { get; set; }
        public int? RequisiteNumber { get; set; }
        public int? Code { get; set; }
        public string? ReferenceBookName { get; set; }
        public string? KeyWordName { get; set; }
        public string? ExplanationNumber { get; set; }
        public DateTime? ExplanationDate { get; set; }
        public string? OrganizationName { get; set; }
        public string? RegionsDivisionName { get; set; }
        public string? Event { get; set; }
		public string? Comment { get; set; }
		public string? ApproveLevel { get; set; }
		public bool? IsAgreedGenProk { get; set; }
		public bool? IsRevelant { get; set; }
		public bool? IsFavorites { get; set; }
        public bool? IsQuestion { get; set; }
    }
}
