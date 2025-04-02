namespace DiplomMetod.Data.Entites
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Explanation> Explorations { get; set; } = new List<Explanation>();
    }
}
