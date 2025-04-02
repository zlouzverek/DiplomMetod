namespace DiplomMetod.Data.Entites
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Explanation> Explonations { get; set; } = new List<Explanation>();
    }
}
