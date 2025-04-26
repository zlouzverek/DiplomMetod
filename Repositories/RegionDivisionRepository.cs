using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class RegionDivisionRepository : IRegionDivisionRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public RegionDivisionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(RegionDivision entity)
        {
            await _dbContext.Set<RegionDivision>().AddAsync(entity);

            await SaveAsync();

        }
        public async Task<IEnumerable<RegionDivision>> GetAll()
        {
            return await _dbContext.Set<RegionDivision>().ToListAsync();
        }

        public async Task<RegionDivision> GetById(int id)
        {
            return await _dbContext.Set<RegionDivision>().FindAsync(id);
        }

        public async Task Remove(RegionDivision entity)
        {
            _dbContext.Set<RegionDivision>().Remove(entity);
            await SaveAsync();
        }

        public async Task Update(RegionDivision entity)
        {
            _dbContext.Set<RegionDivision>().Update(entity);

            await SaveAsync();


        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
