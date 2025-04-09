using DiplomMetod.Data.Entites;//пространство имен,гдетнаходится класс организаций

namespace DiplomMetod.Repositories//пространосто имен для репозитория
{
    //Добавление интерфейса репозитория Organization
    public interface IOrganizationRepository
    {
        Task Add(Organization entity);// добавление организации
        Task<Organization> GetById(int id);//получение организации по ID
        Task Remove(Organization entity);//удаление
        Task<IEnumerable<Organization>> GetAll();//получение всех организаций
        Task Update(Organization entity);//обновление организаций
    }
}
