using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesmateriaalInhoudRepository : IRepository<LesmateriaalInhoud>
    {
        private readonly DataContext _dataContext;

        public LesmateriaalInhoudRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(LesmateriaalInhoud entity)
        {
            if (entity != null)
            {
                _dataContext.LesmateriaalInhoud.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var lesmateriaalVorm = await _dataContext.LesmateriaalInhoud.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalVorm != null)
            {
                _dataContext.LesmateriaalInhoud.Remove(lesmateriaalVorm);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<LesmateriaalInhoud>> GetAll()
        {
            return await _dataContext.LesmateriaalInhoud
                .Include(l => l.Lesmateriaal)
                .ToListAsync();
        }

        public async Task<LesmateriaalInhoud?> GetById(int id)
        {
            return await _dataContext.LesmateriaalInhoud
                .Where(l => l.Id == id)
                .Include(l => l.Lesmateriaal)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, LesmateriaalInhoud entity)
        {
            var lesmateriaalVorm = await _dataContext.LesmateriaalInhoud.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalVorm != null && entity != null)
            {
                lesmateriaalVorm.LesmateriaalId = entity.LesmateriaalId;
                lesmateriaalVorm.Inhoud = entity.Inhoud;
                lesmateriaalVorm.Versie = await GetNewVersion(id);

                _dataContext.LesmateriaalInhoud.Update(lesmateriaalVorm);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<decimal> GetNewVersion(int id)
        {
            var lestmateriaalVorm = await _dataContext.LesmateriaalVormen.Where(l => l.Id == id).FirstOrDefaultAsync();
            return lestmateriaalVorm!.Versie + 0.01m;
        }
    }
}
