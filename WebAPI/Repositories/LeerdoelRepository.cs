using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LeerdoelRepository : IRepository<Leerdoel>
    {
        private readonly DataContext _dataContext;

        public LeerdoelRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Leerdoel entity)
        {
            if (entity != null)
            {
                var isLeeruitkosmtExisting = await IsExsisting(entity);
                if (isLeeruitkosmtExisting)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Leerdoelen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var leerdoel = await _dataContext.Leerdoelen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (leerdoel != null)
            {
                _dataContext.Remove(leerdoel);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Leerdoel>> GetAll()
        {
            return await _dataContext.Leerdoelen
                .Include(l => l.Onderwijseenheid)
                .Include(l => l.Leeruitkomsten)
                .ToListAsync();
        }

        public async Task<Leerdoel?> GetById(int id)
        {
            return await _dataContext.Leerdoelen
                .Where(l => l.Id == id)
                .Include(l => l.Onderwijseenheid)
                .Include(l => l.Leeruitkomsten)
                .ThenInclude(l => l.Lessen)
                .Include(l => l.Leeruitkomsten)
                .ThenInclude(l => l.Tentamens)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Leerdoel entity)
        {
            var leerdoel = await _dataContext.Leerdoelen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (leerdoel != null && entity != null) 
            {
                var isLeeruitkosmtExisting = await IsExsisting(id, entity);
                if (isLeeruitkosmtExisting)
                {
                    ThrowHttpRequestException();
                }

                leerdoel.OnderwijseenheidId = entity.OnderwijseenheidId;
                leerdoel.Naam = entity.Naam;
                leerdoel.Beschrijving = entity.Beschrijving;

                _dataContext.Leerdoelen.Update(leerdoel);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Leerdoel entity)
        {
            var onderwijseenheden = await GetAll();
            return onderwijseenheden.Any(l => l.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Leerdoel entity)
        {
            var onderwijseenheden = await GetAll();
            return onderwijseenheden.Any(l => l.Naam == entity.Naam && l.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Leerodel met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
