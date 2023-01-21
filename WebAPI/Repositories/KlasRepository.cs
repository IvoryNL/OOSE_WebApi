using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class KlasRepository : IKlasRepository<Klas>
    {
        private readonly DataContext _dataContext;

        public KlasRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Klas entity)
        {
            if (entity != null)
            {
                var isExistingKlas = await IsExsisting(entity);
                if (isExistingKlas)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Klassen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var klas = await _dataContext.Klassen.Where(k => k.Id == id).FirstOrDefaultAsync();
            if (klas != null)
            {
                _dataContext.Klassen.Remove(klas);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Klas>> GetAll()
        {
            return await _dataContext.Klassen
                .Include(k => k.Gebruikers)
                .ToListAsync();
        }

        public async Task<Klas?> GetById(int id)
        {
            return await _dataContext.Klassen
                .Where(k => k.Id == id)
                .Include(k => k.Gebruikers)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Klas>> GetKlassenByOpleidingId(int opleidingId)
        {
            return await _dataContext.Klassen
                .Include(k => k.Onderwijsuitvoeringen
                .Where(o => o.Onderwijsmodule.OpleidingId == opleidingId))
                .Select(k => k)
                .ToListAsync();
        }

        public async Task Update(int id, Klas entity)
        {
            var klas = await _dataContext.Klassen.Where(k => k.Id == id).FirstOrDefaultAsync();
            if (klas != null && entity != null)
            {
                var isExistingKlas = await IsExsisting(id, entity);
                if (isExistingKlas)
                {
                    ThrowHttpRequestException();
                }

                klas.Klasnaam = entity.Klasnaam;

                _dataContext.Klassen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Klas entity)
        {
            var klassen = await GetAll();
            return klassen.Any(a => a.Klasnaam == entity.Klasnaam);
        }

        private async Task<bool> IsExsisting(int id, Klas entity)
        {
            var klassen = await GetAll();
            return klassen.Any(k => k.Klasnaam == entity.Klasnaam && k.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Auteur met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
