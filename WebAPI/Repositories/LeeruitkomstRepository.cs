using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LeeruitkomstRepository : ILeeruitkomstRepository<Leeruitkomst>
    {
        private readonly DataContext _dataContext;

        public LeeruitkomstRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Leeruitkomst entity)
        {
            if (entity != null)
            {
                var isExistingLeeruitkomst = await IsExsisting(entity);
                if (isExistingLeeruitkomst)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Leeruitkomsten.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var leeruitkomst = await _dataContext.Leeruitkomsten.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (leeruitkomst != null)
            {
                _dataContext.Leeruitkomsten.Remove(leeruitkomst); 
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Leeruitkomst>> GetAll()
        {
            return await _dataContext.Leeruitkomsten
                .Include(l => l.Leerdoel)
                .Include(l => l.Beoordelingscriterium)
                .Include(l => l.Tentamens)
                .ToListAsync();
        }

        public async Task<Leeruitkomst?> GetById(int id)
        {
            return await _dataContext.Leeruitkomsten
                .Where(l => l.Id == id)
                .Include(l => l.Leerdoel)
                .Include(l => l.Beoordelingscriterium)
                .Include(l => l.Tentamens)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Leeruitkomst>> GetLeeruitkomstenByOpleidingId(int id)
        {
            return await _dataContext.Leeruitkomsten
                .Include(l => l.Leerdoel)
                .ThenInclude(l => l.Onderwijseenheid)
                .ThenInclude(o => o.Onderwijsmodules.Where(o => o.OpleidingId == id))
                .Select(l => l) .ToListAsync();
        }

        public async Task Update(int id, Leeruitkomst entity)
        {
            var leeruitkomst = await _dataContext.Leeruitkomsten.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (leeruitkomst != null && entity != null)
            {
                var isExistingLeeruitkomst = await IsExsisting(id, entity);
                if (isExistingLeeruitkomst)
                {
                    ThrowHttpRequestException();
                }

                leeruitkomst.LeerdoelId = entity.LeerdoelId;
                leeruitkomst.Naam = entity.Naam;
                leeruitkomst.Beschrijving = entity.Beschrijving;

                _dataContext.Leeruitkomsten.Update(leeruitkomst);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Leeruitkomst entity)
        {
            var leeruitkomsten = await GetAll();
            return leeruitkomsten.Any(l => l.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Leeruitkomst entity)
        {
            var leeruitkomsten = await GetAll();
            return leeruitkomsten.Any(l => l.Naam == entity.Naam && l.Id != id);
        }
        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Leeruitkomst met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
