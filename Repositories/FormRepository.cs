using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class FormRepository : IFormRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public FormRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Form entity)
        {
            await _dbContext.Set<Form>().AddAsync(entity);

            await SaveAsync();

        }
        public async Task<IEnumerable<Form>> GetAll()
        {
            return await _dbContext.Set<Form>().ToListAsync();
        }

        public async Task<Form> GetById(int id)
        {
            return await _dbContext.Set<Form>().FindAsync(id);
        }

        public async Task<IEnumerable<FormType>> GetFormTypes()
        {
            return await _dbContext.Set<FormType>().ToListAsync();
        }

        public async Task Remove(Form entity)
        {
            _dbContext.Set<Form>().Remove(entity); 
            await SaveAsync();
        }

        public async Task Update(Form entity)
        {
            _dbContext.Set<Form>().Update(entity);

            await SaveAsync();


        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
