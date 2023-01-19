using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class VormRepository : IRepository<Vorm>
    {
        private readonly DataContext _dataContext;

        public VormRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Vorm entity)
        {
            if (entity != null)
            {
                var isExistingVorm = await IsExsisting(entity);
                if (isExistingVorm)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Vormen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var vorm = await _dataContext.Vormen.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (vorm != null)
            {
                _dataContext.Vormen.Remove(vorm);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Vorm>> GetAll()
        {
            return await _dataContext.Vormen.ToListAsync();
        }

        public async Task<Vorm?> GetById(int id)
        {
            return await _dataContext.Vormen.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(int id, Vorm entity)
        {
            var vorm = await _dataContext.Vormen.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (vorm != null && entity != null)
            {
                var isExistingVorm = await IsExsisting(id, entity);
                if (isExistingVorm)
                {
                    ThrowHttpRequestException();
                }

                vorm.Naam = entity.Naam;

                _dataContext.Vormen.Update(vorm);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Vorm entity)
        {
            var vormen = await GetAll();
            return vormen.Any(v => v.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Vorm entity)
        {
            var vormen = await GetAll();
            return vormen.Any(v => v.Naam == entity.Naam && v.Id != id);
        }
        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Vorm met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
