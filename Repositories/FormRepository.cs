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
            return await _dbContext.Set<Form>()
                .Include(x => x.Explanation)
                    .ThenInclude(x => x.Organization)
                .Include(x => x.ReferenceBook)
                .Include(x => x.KeyWords)
                .Include(x => x.FormType)
                .Include(x => x.RegionsDivision)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Form> GetById(int id)
        {
            return await _dbContext.Set<Form>().Include(x => x.Explanation)
                               .ThenInclude(x => x.Organization)
                               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Form> GetQueryAllWithIncludes()
        {
            return Query().Include(f => f.FormType)
                .Include(f => f.ReferenceBook)
                .Include(f => f.KeyWords)
                .Include(f => f.Explanation)
                    .ThenInclude(e => e.Organization)
                .Include(f => f.RegionsDivision);
        }

        public IQueryable<Form> Query()
        {
            return _dbContext.Set<Form>().AsQueryable();
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
