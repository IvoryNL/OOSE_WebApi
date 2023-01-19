using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class BeoordelingsonderdeelRepository : IRepository<Beoordelingsonderdeel>
    {
        private readonly DataContext _dataContext;

        public BeoordelingsonderdeelRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Beoordelingsonderdeel entity)
        {
            if (entity != null)
            {
                var isExistingBeoordelingsonderdeel = await IsExsisting(entity);
                if (isExistingBeoordelingsonderdeel)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Beoordelingsonderdelen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var beoordelingsonderdeel = await _dataContext.Beoordelingsonderdelen.Where(b => b.Id == id).Include(b => b.Beoordelingscriterium).FirstOrDefaultAsync();
            if (beoordelingsonderdeel != null)
            {
                _dataContext.Beoordelingsonderdelen.Remove(beoordelingsonderdeel);
                await _dataContext.SaveChangesAsync();
            }
        }

        public Task<List<Beoordelingsonderdeel>> GetAll()
        {
            return _dataContext.Beoordelingsonderdelen
                .Include(b => b.Beoordelingsmodel)
                .Include(b => b.Beoordelingscriterium)
                .ToListAsync();
        }

        public Task<Beoordelingsonderdeel?> GetById(int id)
        {
            return _dataContext.Beoordelingsonderdelen
                .Where(b => b.Id == id)
                .Include(b => b.Beoordelingsmodel)
                .Include(b => b.Beoordelingscriterium)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Beoordelingsonderdeel entity)
        {
            var beoordelingsonderdeel = await _dataContext.Beoordelingsonderdelen.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingsonderdeel != null && entity != null)
            {
                var isExistingBeoordelingsonderdeel = await IsExsisting(id, entity);
                if (isExistingBeoordelingsonderdeel)
                {
                    ThrowHttpRequestException();
                }

                beoordelingsonderdeel.BeoordelingsmodelId = entity.BeoordelingsmodelId;
                beoordelingsonderdeel.Titel = entity.Titel;
                beoordelingsonderdeel.Resultaat = entity.Resultaat;
                beoordelingsonderdeel.Gewicht = entity.Gewicht;
                beoordelingsonderdeel.Grens = entity.Grens;
                beoordelingsonderdeel.Verplicht = entity.Verplicht;

                _dataContext.Beoordelingsonderdelen.Update(beoordelingsonderdeel);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Beoordelingsonderdeel entity)
        {
            var eoordelingsonderdelen = await GetAll();
            return eoordelingsonderdelen.Any(b => b.Titel == entity.Titel);
        }

        private async Task<bool> IsExsisting(int id, Beoordelingsonderdeel entity)
        {
            var eoordelingsonderdelen = await GetAll();
            return eoordelingsonderdelen.Any(b => b.Titel == entity.Titel && b.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Beoordelingsonderdeel met deze titel.";
            throw new HttpRequestException(message);
        }
    }
}
