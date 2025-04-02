using System.ComponentModel;

namespace DiplomMetod.Data.Entites
{
    public class ReferenceBook : BaseEntity
    {

        [Description("Номер справочника")]
        public string Name { get; set; }

        [Description("Полное наименование справочника")]
        public string FullName { get; set; }

        public ICollection<Form>? Forms { get; set; }
    }
}
