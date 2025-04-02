using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        protected readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
           
            await SaveAsync();

        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
           
            await SaveAsync();


        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
