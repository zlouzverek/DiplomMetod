namespace DiplomMetod.Data.Entites
{
    public class FormType : BaseEntity
    {
        public string Name { get; set; }

        public int FormId { get; set; }

        public ICollection<Form>? Forms { get; set; }
    }
}
