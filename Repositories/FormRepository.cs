using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class FormRepository : GenericRepository<Form>, IFormRepository
    {
        public FormRepository(DbContext dbContext) : base(dbContext)
        {

        }

        //public async Task<Form> GetByName(string name)
        //{
        //    return await _dbContext.FindAsync(name);
        //}

    }
}
