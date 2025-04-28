using DiplomMetod.Data.Entites;

namespace DiplomMetod.Repositories
{
    public interface IFormRepository
    {
        Task Add(Form entity);

        Task<Form> GetById(int id);

        Task Remove(Form entity);

        Task<IEnumerable<Form>> GetAll();

        Task Update(Form entity);

        IQueryable<Form> Query();

        IQueryable<Form> GetQueryAllWithIncludes();


    }
}