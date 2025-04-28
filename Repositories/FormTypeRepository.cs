using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class FormTypeRepository : IFormTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FormTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(FormType entity)
        {
            await _dbContext.Set<FormType>().AddAsync(entity);

            await SaveAsync();
        }

        public async Task<IEnumerable<FormType>> GetAll()
        {
            return await _dbContext.Set<FormType>().ToListAsync();
        }

        public async Task<FormType> GetById(int id)
        {
            return await _dbContext.Set<FormType>().FindAsync(id);
        }

        public async Task Remove(FormType entity)
        {
            _dbContext.Set<FormType>().Remove(entity);
            await SaveAsync();
        }

        public async Task Update(FormType entity)
        {
            _dbContext.Set<FormType>().Update(entity);

            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
