using DiplomMetod.Data.Entites;

namespace DiplomMetod.Repositories
{
    public interface IRegionDivisionRepository
    {
        Task Add(RegionDivision entity);
        Task<RegionDivision> GetById(int id);
        Task Remove(RegionDivision entity);
        Task<IEnumerable<RegionDivision>> GetAll();
        Task Update(RegionDivision entity);
    }
}
