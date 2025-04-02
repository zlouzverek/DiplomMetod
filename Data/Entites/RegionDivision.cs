
namespace DiplomMetod.Data.Entites
{
    public class RegionDivision : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Form>? Forms { get; set; }

    }
}
