using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class OpleidingRepository : IRepository<Opleiding>
    {
        private readonly DataContext _dataContext;

        public OpleidingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Opleiding entity)
        {
            if (entity != null)
            {
                var isExistingOpleiding = await IsExsisting(entity);
                if (isExistingOpleiding)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Opleidingen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var opleiding = await _dataContext.Opleidingen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (opleiding != null)
            {
                _dataContext.Opleidingen.Remove(opleiding);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Opleiding>> GetAll()
        {
            return await _dataContext.Opleidingen
                .Include(o => o.Opleidingsprofielen)
                .Include(o => o.Onderwijsmodules)
                .Include(o => o.Vorm)
                .ToListAsync();
        }

        public async Task<Opleiding?> GetById(int id)
        {
            return await _dataContext.Opleidingen
                .Where(o => o.Id == id)
                .Include(o => o.Opleidingsprofielen)
                .Include(o => o.Onderwijsmodules)
                .Include(o => o.Vorm)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Opleiding entity)
        {
            var opleiding = await _dataContext.Opleidingen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (opleiding != null && entity != null)
            {
                var isExistingOpleiding = await IsExsisting(id, entity);
                if (isExistingOpleiding)
                {
                    ThrowHttpRequestException();
                }

                opleiding.VormId = entity.VormId;
                opleiding.Naam = entity.Naam;

                _dataContext.Opleidingen.Update(opleiding);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Opleiding entity)
        {
            var opleidingen = await GetAll();
            return opleidingen.Any(o => o.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Opleiding entity)
        {
            var opleidingen = await GetAll();
            return opleidingen.Any(o => o.Naam == entity.Naam && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Opleiding met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
