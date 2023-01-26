using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class BeoordelingscriteriaRepository : IRepository<Beoordelingscriteria>
    {
        private readonly DataContext _dataContext;

        public BeoordelingscriteriaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Beoordelingscriteria entity)
        {
            if (entity != null)
            {
                var isExistingBeoordelingscriteria = await IsExsisting(entity);
                if (isExistingBeoordelingscriteria)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Beoordelingscriterium.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var beoordelingscriteria = await _dataContext.Beoordelingscriterium.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingscriteria != null)
            {
                _dataContext.Beoordelingscriterium.Remove(beoordelingscriteria);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Beoordelingscriteria>> GetAll()
        {
            return await _dataContext.Beoordelingscriterium
                .Include(b => b.Beoordelingsonderdeel)
                .Include(b => b.Leeruitkomst)
                .Include(b => b.Beoordelingsdimensies)
                .ToListAsync();
        }

        public async Task<Beoordelingscriteria?> GetById(int id)
        {
            return await _dataContext.Beoordelingscriterium
                .Where(b => b.Id == id)
                .Include(b => b.Beoordelingsonderdeel)
                .Include(b => b.Leeruitkomst)
                .Include(b => b.Beoordelingsdimensies)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Beoordelingscriteria entity)
        {
            var beoordelingscriteria = await _dataContext.Beoordelingscriterium.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingscriteria != null && entity != null)
            {
                var isExistingBeoordelingscriteria = await IsExsisting(id, entity);
                if (isExistingBeoordelingscriteria)
                {
                    ThrowHttpRequestException();
                }

                beoordelingscriteria.BeoordelingsonderdeelId = entity.BeoordelingsonderdeelId;
                beoordelingscriteria.LeeruitkomstId = entity.LeeruitkomstId;
                beoordelingscriteria.Criteria = entity.Criteria;
                beoordelingscriteria.Omschrijving = entity.Omschrijving;
                beoordelingscriteria.Resultaat = entity.Resultaat;
                beoordelingscriteria.Gewicht = entity.Gewicht;
                beoordelingscriteria.Grens = entity.Grens;
                beoordelingscriteria.Verplicht = entity.Verplicht;

                _dataContext.Beoordelingscriterium.Update(beoordelingscriteria); 
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Beoordelingscriteria entity)
        {
            var beoordelingscriterium = await GetAll();
            return beoordelingscriterium.Any(b => b.Criteria == entity.Criteria);
        }

        private async Task<bool> IsExsisting(int id, Beoordelingscriteria entity)
        {
            var beoordelingscriterium = await GetAll();
            return beoordelingscriterium.Any(b => b.Criteria == entity.Criteria && b.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Beoordelingscriteria met deze criteria.";
            throw new HttpRequestException(message);
        }
    }
}
