using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomMetod.Data.Entites
{
    public class Explanation : BaseEntity
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string? Number { get; set; }

        public DateTime Data { get; set; }
        public Organization Organization { get; set; }

        /// <summary>
        /// Согласовано с ГП
        /// </summary>
        public bool IsAgreedGenProk { get; set; }

        /// <summary>
        ///  Актуальность
        /// </summary>
        public bool IsRevelant { get; set; }

        [Description("Пример из жизни")]
        public string? Description { get; set; }

        [Display(Name = "Примечание")]
        public string? Comment { get; set; }

        [Display(Name = "Избранное")]
        public bool IsFavorites { get; set; }

        public Form Form { get; set; }
        public int FormId { get; set; }

        [Description("Уровень согласования")]
        public ApproveLevel ApproveLevel { get; set; }

    }
}
