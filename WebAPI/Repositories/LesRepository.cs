using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesRepository : IRepository<Les>
    {
        private readonly DataContext _dataContext;

        public LesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Les entity)
        {
            if (entity != null)
            {
                _dataContext.Lessen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (les != null) 
            { 
                _dataContext.Lessen.Remove(les);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Les>> GetAll()
        {
            return await _dataContext.Lessen
                .Include(l => l.Leeruitkomsten)
                .Include(l => l.Lesmaterialen)
                .ToListAsync();
        }

        public async Task<Les?> GetById(int id)
        {
            return await _dataContext.Lessen
                .Where(l => l.Id == id)
                .Include(l => l.Leeruitkomsten)
                .Include(l => l.Lesmaterialen)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Les entity)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (les != null && entity != null)
            {
                les.Omschrijving = entity.Omschrijving;

                _dataContext.Lessen.Update(les);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
