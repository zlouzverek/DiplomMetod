using DiplomMetod.Data.Entites;

namespace DiplomMetod.Models
{
    public class FormSearchFilter
    {
        public string InventoryNumber { get; set; }

        //#FIXME: Правильно ли указал наимееования//
        public int FormId { get; set; }

        public int? RequisiteNumber { get; set; }
        public int? Code { get; set; }

        public int ReferenceBooksId { get; set; }

        public string KeyWords { get; set; }

        public string? ExplanationNumber { get; set; }

        public DateTime ExplanationData { get; set; }

        public int OrganizationId { get; set; }

        public bool IsAgreedGenProk { get; set; }

        public ApproveLevel ApproveLevel { get; set; }

        public bool IsRevelant { get; set; }

        public int RegionsDivisionsId { get; set; }

        public string? Comment { get; set; }

        public bool IsFavorites { get; set; }
    }
}
