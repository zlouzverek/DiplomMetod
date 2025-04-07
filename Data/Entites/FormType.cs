using System.ComponentModel;

namespace DiplomMetod.Data.Entites
{
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
