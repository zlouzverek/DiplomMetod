using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace DiplomMetod.Data.Entites
{
    public class Form : BaseEntity
    {
        public int FormTypeId { get; set; }
        public FormType FormType { get; set; }
        public string InventoryNumber
        {
            get
            {
                return this.InventoryNumber;

            }

            set
            {
                value = $"{Id}_{FormType.Name}";
            }
        }

        [Description("Номер реквизита")]
        public int? RequisiteNumber { get; set; }

        [Description("Номер кода")]
        public int? Code { get; set; }

        [Description("Уровень согласования")]
        public ApproveLevel ApproveLevel { get; set; }

        public int ReferenceBooksId { get; set; }

        public ReferenceBook? ReferenceBook { get; set; }

        public ICollection<KeyWord> KeyWords { get; set; }

        public Explanation Explanation { get; set; }

        public int ExplanationId { get; set; }

        public int RegionsDivisionsId { get; set; }

        public RegionDivision? RegionsDivision { get; set; }

    }
}
