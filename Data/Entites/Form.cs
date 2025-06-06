﻿using System.ComponentModel;

namespace DiplomMetod.Data.Entites
{
    public class Form : BaseEntity
    {
        public int FormTypeId { get; set; }
        public FormType FormType { get; set; }

        [Description("Номер реквизита")]
        public int? RequisiteNumber { get; set; }

        [Description("Номер кода")]
        public int? Code { get; set; }

        public int ReferenceBooksId { get; set; }

        public ReferenceBook? ReferenceBook { get; set; }

        public ICollection<KeyWord> KeyWords { get; set; } = new List<KeyWord>();

        public Explanation Explanation { get; set; }

        public int ExplanationId { get; set; }

        public int RegionsDivisionsId { get; set; }

        public RegionDivision? RegionsDivision { get; set; }

        public string? Event { get; set; }

        public string? Question { get; set; }

        public string? Answer { get; set; }

        public string? FileLink { get; set; }

        public bool IsQuestion { get; set; }

    }
}
