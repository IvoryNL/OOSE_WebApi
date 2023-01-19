using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesmateriaalVormRepository : IRepository<LesmateriaalVorm>
    {
        private readonly DataContext _dataContext;

        public LesmateriaalVormRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(LesmateriaalVorm entity)
        {
            if (entity != null)
            {
                _dataContext.LesmateriaalVormen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var lesmateriaalVorm = await _dataContext.LesmateriaalVormen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalVorm != null)
            {
                _dataContext.LesmateriaalVormen.Remove(lesmateriaalVorm);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<LesmateriaalVorm>> GetAll()
        {
            return await _dataContext.LesmateriaalVormen
                .Include(l => l.Lesmateriaal)
                .ToListAsync();
        }

        public async Task<LesmateriaalVorm?> GetById(int id)
        {
            return await _dataContext.LesmateriaalVormen
                .Where(l => l.Id == id)
                .Include(l => l.Lesmateriaal)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, LesmateriaalVorm entity)
        {
            var lesmateriaalVorm = await _dataContext.LesmateriaalVormen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalVorm != null && entity != null)
            {
                lesmateriaalVorm.LesmateriaalId = entity.LesmateriaalId;
                lesmateriaalVorm.Structuur = entity.Structuur;
                lesmateriaalVorm.Bestandstype = entity.Bestandstype;
                lesmateriaalVorm.Versie = await GetNewVersion(id);

                _dataContext.LesmateriaalVormen.Update(lesmateriaalVorm);
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
