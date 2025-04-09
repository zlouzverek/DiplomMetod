using DiplomMetod.Data;
using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Repositories
{
    public class OrganizationRepository : IOrganizationRepository//создание базового CRUD-репозитория (добавить (create),получить (read), обновить (update), удалить (delete)) для работы с таблицей организаций
    {

        private readonly ApplicationDbContext _dbContext;
        public OrganizationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Organization entity)//метод для добавления новой записии в таблицу организаций и асинхронного сохранения
        {
            await _dbContext.Set<Organization>().AddAsync(entity);

            await SaveAsync();

        }
        public async Task<IEnumerable<Organization>> GetAll()//загрузка всех записей из таблицы оргннаизаций
        {
            return await _dbContext.Set<Organization>().ToListAsync();
        }

        public async Task<Organization> GetById(int id)//поиск записи по ID
        {
            return await _dbContext.Set<Organization>().FindAsync(id);
        }

        public async Task Remove(Organization entity)//удаление и сохранение
        {
            _dbContext.Set<Organization>().Remove(entity);
            await SaveAsync();
        }

        public async Task Update(Organization entity)//обнновление и сохранение
        {
            _dbContext.Set<Organization>().Update(entity);

            await SaveAsync();


        }

        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
