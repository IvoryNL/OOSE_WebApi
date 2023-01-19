using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class PlanningRepository : IRepository<Planning>
    {
        private readonly DataContext _dataContext;

        public PlanningRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Planning entity)
        {
            if (entity != null)
            {
                var isExistingPlanning = await IsExsisting(entity);
                if (isExistingPlanning)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Planningen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var planning = await _dataContext.Planningen.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (planning != null)
            {
                _dataContext.Planningen.Remove(planning);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Planning>> GetAll()
        {
            return await _dataContext.Planningen
                .Include(p => p.Onderwijsuitvoering)
                .Include(p => p.Tentamens)
                .Include(p => p.Lessen)
                .ToListAsync();
        }

        public async Task<Planning?> GetById(int id)
        {
            return await _dataContext.Planningen
                .Where(p => p.Id == id)
                .Include(p => p.Onderwijsuitvoering)
                .Include(p => p.Tentamens)
                .Include(p => p.Lessen)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Planning entity)
        {
            var planning = await _dataContext.Planningen.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (planning != null && entity != null)
            {
                var isExistingPlanning = await IsExsisting(id, entity);
                if (isExistingPlanning)
                {
                    ThrowHttpRequestException();
                }

                planning.OnderwijsuitvoeringId = entity.OnderwijsuitvoeringId;
                planning.Datum = entity.Datum;
                planning.Weeknummer = entity.Weeknummer;

                _dataContext.Planningen.Update(planning);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Planning entity)
        {
            var planningen = await GetAll();
            return planningen.Any(p => p.Datum == entity.Datum && p.Weeknummer == entity.Weeknummer);
        }

        private async Task<bool> IsExsisting(int id, Planning entity)
        {
            var planningen = await GetAll();
            return planningen.Any(p => p.Datum == entity.Datum && p.Weeknummer == entity.Weeknummer && p.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Planning met deze datum en weeknummer.";
            throw new HttpRequestException(message);
        }
    }
}
