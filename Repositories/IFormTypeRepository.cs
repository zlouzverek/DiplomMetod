using DiplomMetod.Data.Entites;

namespace DiplomMetod.Repositories
{
    public interface IFormTypeRepository
    {
            Task Add(FormType entity);

            Task<FormType> GetById(int id);

            Task Remove(FormType entity);

            Task<IEnumerable<FormType>> GetAll();

            Task Update(FormType entity);
    }
}
