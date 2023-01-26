using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class OnderwijseenheidRepository : IRepository<Onderwijseenheid>
    {
        private readonly DataContext _dataContext;

        public OnderwijseenheidRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Onderwijseenheid entity)
        {
            if (entity != null)
            {
                var isExistingOnderwijseenheid = await IsExsisting(entity);
                if (isExistingOnderwijseenheid)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Onderwijseenheden.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var onderwijseenheid = await _dataContext.Onderwijseenheden.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijseenheid != null)
            {
                _dataContext.Onderwijseenheden.Remove(onderwijseenheid);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Onderwijseenheid>> GetAll()
        {
            return await _dataContext.Onderwijseenheden
                .Include(o => o.Tentamens)
                .Include(o => o.Leerdoelen)
                .ThenInclude(l => l.Leeruitkomsten)
                .ToListAsync();
        }

        public async Task<Onderwijseenheid?> GetById(int id)
        {
            return await _dataContext.Onderwijseenheden
                .Where(o => o.Id == id)
                .Include(o => o.Tentamens)
                .Include(o => o.Leerdoelen)
                .ThenInclude(l => l.Leeruitkomsten)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Onderwijseenheid entity)
        {
            var onderwijseenheid = await _dataContext.Onderwijseenheden.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijseenheid != null && entity != null)
            {
                var isExistingOnderwijseenheid = await IsExsisting(id, entity);
                if (isExistingOnderwijseenheid)
                {
                    ThrowHttpRequestException();
                }

                onderwijseenheid.Naam = entity.Naam;
                onderwijseenheid.Beschrijving = entity.Beschrijving;
                onderwijseenheid.Coordinator = entity.Coordinator;
                onderwijseenheid.Studiepunten = entity.Studiepunten;

                _dataContext.Onderwijseenheden.Update(onderwijseenheid);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Onderwijseenheid entity)
        {
            var onderwijseenheden = await GetAll();
            return onderwijseenheden.Any(o => o.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Onderwijseenheid entity)
        {
            var onderwijseenheden = await GetAll();
            return onderwijseenheden.Any(o => o.Naam == entity.Naam && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Onderwijseenheid met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
