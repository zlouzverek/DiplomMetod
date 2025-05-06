using System.ComponentModel;

namespace DiplomMetod.Models
{
    public class FormExportModel
    {
        [Description("Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Description("Тип формы")]
        public string FormTypeName { get; set; }

        [Description("Номер реквизита")]
        public int? RequisiteNumber { get; set; }

        [Description("Номер кода")]
        public int? Code { get; set; }

        [Description("Справочник")]
        public string ReferenceBookName { get; set; }

        [Description("Ключевые слова")]
        public string KeyWords { get; set; }

        [Description("№ разъяснения")]
        public string ExplanationNumber { get; set; }

        [Description("Дата разъяснения")]
        public DateTime ExplanationDate { get; set; }

        [Description("Кто разъяснил")]
        public string OrganizationName { get; set; }

		[Description("Кому разъяснено")]
		public string RegionDivisionName { get; set; }

		[Description("Примечание")]
		public string Comment { get; set; }

        [Description("Пример из жизни")]
        public string? Description { get; set; }

        [Description("Уровень")]
		public string ApproveLevel { get; set; }

		[Description("Согласовано с ГП")]
        public string IsAgreedGenProk { get; set; }

        [Description("Актуальность")]
        public string IsRevelant { get; set; }

        [Description("Избранное")]
        public string IsFavorites { get; set; }

    }
}
