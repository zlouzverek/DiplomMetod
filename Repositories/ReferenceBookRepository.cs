using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class ReferenceBookRepository : IReferenceBookRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public ReferenceBookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(ReferenceBook entity)
        {
            await _dbContext.Set<ReferenceBook>().AddAsync(entity);

            await SaveAsync();

        }
        public async Task<IEnumerable<ReferenceBook>> GetAll()
        {
            return await _dbContext.Set<ReferenceBook>().ToListAsync();
        }

        public async Task<ReferenceBook> GetById(int id)
        {
            return await _dbContext.Set<ReferenceBook>().FindAsync(id);
        }

        public async Task Remove(ReferenceBook entity)
        {
            _dbContext.Set<ReferenceBook>().Remove(entity);
            await SaveAsync();
        }

        public async Task Update(ReferenceBook entity)
        {
            _dbContext.Set<ReferenceBook>().Update(entity);

            await SaveAsync();


        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
