using DiplomMetod.Data.Entites;

namespace DiplomMetod.Repositories
{
    public interface IReferenceBookRepository
    {
        Task Add(ReferenceBook entity);
        Task<ReferenceBook> GetById(int id);
        Task Remove(ReferenceBook entity);
        Task<IEnumerable<ReferenceBook>> GetAll();
        Task Update(ReferenceBook entity);
    }
}
