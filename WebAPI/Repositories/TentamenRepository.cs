using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class TentamenRepository : IRepository<Tentamen>
    {
        private readonly DataContext _dataContext;

        public TentamenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Tentamen entity)
        {
            if (entity != null)
            {
                var isExistingTentamen = await IsExsisting(entity);
                if (isExistingTentamen)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Tentamens.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tentamen != null)
            {
                _dataContext.Tentamens.Remove(tentamen);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Tentamen>> GetAll()
        {
            return await _dataContext.Tentamens
                .Include(t => t.Vorm)
                .Include(t => t.Beoordelingsmodel)
                .Include(t => t.Leeruitkomsten)
                .ToListAsync();
        }

        public async Task<Tentamen?> GetById(int id)
        {
            return await _dataContext.Tentamens
                .Where(t => t.Id == id)
                .Include(t => t.Vorm)
                .Include(t => t.Beoordelingsmodel)
                .Include(t => t.Leeruitkomsten)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Tentamen entity)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tentamen != null && entity != null)
            {
                var isExistingTentamen = await IsExsisting(id, entity);
                if (isExistingTentamen)
                {
                    ThrowHttpRequestException();
                }

                tentamen.VormId = entity.VormId;
                tentamen.Naam = entity.Naam;

                _dataContext.Tentamens.Update(tentamen);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Tentamen entity)
        {
            var tentamens = await GetAll();
            return tentamens.Any(t => t.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Tentamen entity)
        {
            var tentamens = await GetAll();
            return tentamens.Any(t => t.Naam == entity.Naam && t.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Tentamen met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
