using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class BeoordelingsdimensieRepository : IRepository<Beoordelingsdimensie>
    {
        private readonly DataContext _dataContext;

        public BeoordelingsdimensieRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Beoordelingsdimensie entity)
        {
            if (entity != null)
            {
                var isExistingBeoordelingsdimensie = await IsExsisting(entity);
                if (isExistingBeoordelingsdimensie)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Beoordelingsdimensies.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var beoordelingsdimensie = await _dataContext.Beoordelingsdimensies.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingsdimensie != null)
            {
                _dataContext.Beoordelingsdimensies.Remove(beoordelingsdimensie);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Beoordelingsdimensie>> GetAll()
        {
            return await _dataContext.Beoordelingsdimensies
                .Include(b => b.Beoordelingscriteria)
                .ToListAsync();
        }

        public async Task<Beoordelingsdimensie?> GetById(int id)
        {
            return await _dataContext.Beoordelingsdimensies
                .Where(b => b.Id == id)
                .Include(b => b.Beoordelingscriteria)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Beoordelingsdimensie entity)
        {
            var beoordelingsdimensie = await _dataContext.Beoordelingsdimensies.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingsdimensie != null && entity != null)
            {
                var isExistingBeoordelingsdimensie = await IsExsisting(id, entity);
                if (isExistingBeoordelingsdimensie)
                {
                    ThrowHttpRequestException();
                }

                beoordelingsdimensie.BeoordelingscriteriaId = entity.BeoordelingscriteriaId;
                beoordelingsdimensie.Titel= entity.Titel;
                beoordelingsdimensie.Omschrijving = entity.Omschrijving;
                beoordelingsdimensie.Toelichting = entity.Toelichting;
                beoordelingsdimensie.Cijferwaarde = entity.Cijferwaarde;

                _dataContext.Beoordelingsdimensies.Update(beoordelingsdimensie);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Beoordelingsdimensie entity)
        {
            var beoordelingsdimensies = await GetAll();
            return beoordelingsdimensies.Any(b => b.Titel == entity.Titel);
        }

        private async Task<bool> IsExsisting(int id, Beoordelingsdimensie entity)
        {
            var beoordelingsdimensies = await GetAll();
            return beoordelingsdimensies.Any(b => b.Titel == entity.Titel && b.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een beoordelingsdimensie met deze titel.";
            throw new HttpRequestException(message);
        }
    }
}
