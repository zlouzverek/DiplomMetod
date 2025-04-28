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

        [Description("Организация")]
        public string OrganizationName { get; set; }

        [Description("Согласовано с ГП")]
        public string IsAgreedGenProk { get; set; }

        [Description("Уровень")]
        public string ApproveLevel { get; set; }

        [Description("Актуальность")]
        public string IsRevelant { get; set; }

        [Description("Регион")]
        public string RegionDivisionName { get; set; }

        [Description("Примечание")]
        public string Comment { get; set; }

        [Description("Избранное")]
        public string IsFavorites { get; set; }

    }
}
