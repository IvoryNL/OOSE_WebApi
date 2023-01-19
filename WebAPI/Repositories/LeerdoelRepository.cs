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
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Leerdoel entity)
        {
            var leerdoel = await _dataContext.Leerdoelen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (leerdoel != null && entity != null) 
            {
                leerdoel.OnderwijseenheidId = entity.OnderwijseenheidId;
                leerdoel.Beschrijving = entity.Beschrijving;

                _dataContext.Leerdoelen.Update(leerdoel);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
