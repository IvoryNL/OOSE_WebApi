using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class BeoordelingRepository : IRepository<Beoordeling>
    {
        private readonly DataContext _dataContext;

        public BeoordelingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Beoordeling entity)
        {
            if (entity != null)
            {
                _dataContext.Beoordelingen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var beoordeling = await _dataContext.Beoordelingen.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordeling != null)
            {
                _dataContext.Beoordelingen.Remove(beoordeling);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Beoordeling>> GetAll()
        {
            return await _dataContext.Beoordelingen
                .Include(b => b.TentamenUpload)
                .Include(b => b.Docent)
                .Include(b => b.Status)
                .ToListAsync();
        }

        public async Task<Beoordeling?> GetById(int id)
        {
            return await _dataContext.Beoordelingen
                .Where(b =>b.Id == id)
                .Include(b => b.TentamenUpload)
                .Include(b => b.Docent)
                .Include(b => b.Status)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Beoordeling entity)
        {
            var beoordeling = await _dataContext.Beoordelingen.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (beoordeling != null && entity != null)
            {
                beoordeling.Id = entity.Id;
                beoordeling.DocentId = entity.DocentId;
                beoordeling.BeoordelingsmodelId = entity.BeoordelingsmodelId;
                beoordeling.StatusId = entity.StatusId;
                beoordeling.Datum = entity.Datum;
                beoordeling.Resultaat = entity.Resultaat;

                _dataContext.Beoordelingen.Update(beoordeling);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
