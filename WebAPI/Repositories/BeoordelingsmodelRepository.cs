using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class BeoordelingsmodelRepository : IRepository<Beoordelingsmodel>
    {
        private readonly DataContext _dataContext;

        public BeoordelingsmodelRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Beoordelingsmodel entity)
        {
            if (entity != null)
            {
                var isExistingBeoordelingsmodel = await IsExsisting(entity);
                if (isExistingBeoordelingsmodel)
                {
                    ThrowHttpRequestException();
                }

                entity.StatusId = 3;
                _dataContext.Beoordelingsmodellen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var beoordelingsmodel = await _dataContext.Beoordelingsmodellen.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingsmodel != null)
            {
                _dataContext.Beoordelingsmodellen.Remove(beoordelingsmodel);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Beoordelingsmodel>> GetAll()
        {
            return await _dataContext.Beoordelingsmodellen
                .Include(b => b.Tentamen)
                .Include(b => b.Docent)
                .Include(b => b.Status)
                .Include(b => b.Beoordelingsonderdelen)
                .ToListAsync();
        }

        public async Task<Beoordelingsmodel?> GetById(int id)
        {
            return await _dataContext.Beoordelingsmodellen
                .Where(b => b.Id == id)
                .Include(b => b.Tentamen)
                .Include(b => b.Docent)
                .Include(b => b.Status)
                .Include(b => b.Beoordelingsonderdelen)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Beoordelingsmodel entity)
        {
            var beoordelingsmodel = await _dataContext.Beoordelingsmodellen.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordelingsmodel != null)
            {
                var isExistingBeoordelingsmodel = await IsExsisting(id, entity);
                if (isExistingBeoordelingsmodel)
                {
                    ThrowHttpRequestException();
                }

                beoordelingsmodel.TentamenId = entity.TentamenId;
                beoordelingsmodel.DocentId = entity.DocentId;
                beoordelingsmodel.StatusId = entity.StatusId;

                _dataContext.Beoordelingsmodellen.Update(beoordelingsmodel); 
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Beoordelingsmodel entity)
        {
            var onderwijsmodules = await GetAll();
            return onderwijsmodules.Any(o => o.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Beoordelingsmodel entity)
        {
            var onderwijsmodules = await GetAll();
            return onderwijsmodules.Any(o => o.Naam == entity.Naam && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Beoordelingsmodel met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
