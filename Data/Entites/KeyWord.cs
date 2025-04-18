namespace DiplomMetod.Data.Entites
{
    public class KeyWord : BaseEntity
    {
        public string Name { get; set; }

        public int FormId { get; set; }

        public Form Form { get; set; }
    }
}
