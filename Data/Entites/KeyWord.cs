namespace DiplomMetod.Data.Entites
{
    public class KeyWord : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Form> Forms { get; set; }
    }
}
