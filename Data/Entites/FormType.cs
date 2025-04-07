using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomMetod.Data.Entites
{
    [Table("FormType")]
    public class FormType : BaseEntity
    {
        [Description("Номер формы")]
        public string Name { get; set; }

        [Description("Полное наименование формы")]
        public string FullName { get; set; }

        public int FormId { get; set; }

        public ICollection<Form>? Forms { get; set; }
    }
}
