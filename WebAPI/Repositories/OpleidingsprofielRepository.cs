using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class OpleidingsprofielRepository : IOpleidingsprofielRepository<Opleidingsprofiel>
    {
        private readonly DataContext _dataContext;

        public OpleidingsprofielRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Opleidingsprofiel entity)
        {
            if (entity != null)
            {
                var isExistingOpleidingsprofiel = await IsExsisting(entity);
                if (isExistingOpleidingsprofiel)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Opleidingsprofielen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var opleidingsprofiel = await _dataContext.Opleidingsprofielen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (opleidingsprofiel != null)
            {
                _dataContext.Opleidingsprofielen.Remove(opleidingsprofiel);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Opleidingsprofiel>> GetAll()
        {
            return await _dataContext.Opleidingsprofielen
                .Include(o => o.Opleiding)
                .ToListAsync();
        }

        public async Task<List<Opleidingsprofiel>> GetAllOpleidingsprofielenByOpleidingId(int opleidingId)
        {
            return await _dataContext.Opleidingsprofielen
                .Where(o => o.OpleidingId == opleidingId)
                .ToListAsync();
        }

        public async Task<Opleidingsprofiel?> GetById(int id)
        {
            return await _dataContext.Opleidingsprofielen
                .Where(o => o.Id == id)
                .Include(o => o.Opleiding)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Opleidingsprofiel entity)
        {
            var opleidingsprofiel = await _dataContext.Opleidingsprofielen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (opleidingsprofiel != null && entity != null)
            {
                var isExistingOpleidingsprofiel = await IsExsisting(id, entity);
                if (isExistingOpleidingsprofiel)
                {
                    ThrowHttpRequestException();
                }

                opleidingsprofiel.OpleidingId = entity.OpleidingId;
                opleidingsprofiel.Profielnaam = entity.Profielnaam;
                opleidingsprofiel.Beschrijving = entity.Beschrijving;

                _dataContext.Opleidingsprofielen.Update(opleidingsprofiel);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Opleidingsprofiel entity)
        {
            var opleidingsprofielen = await GetAll();
            return opleidingsprofielen.Any(o => o.Profielnaam == entity.Profielnaam);
        }

        private async Task<bool> IsExsisting(int id, Opleidingsprofiel entity)
        {
            var opleidingsprofielen = await GetAll();
            return opleidingsprofielen.Any(o => o.Profielnaam == entity.Profielnaam && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Opleidingsprofiel met deze profielnaam.";
            throw new HttpRequestException(message);
        }
    }
}
