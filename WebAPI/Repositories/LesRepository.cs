using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesRepository : ILesRepository<Les>
    {
        private readonly DataContext _dataContext;

        public LesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Les entity)
        {
            if (entity != null)
            {
                _dataContext.Lessen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (les != null) 
            { 
                _dataContext.Lessen.Remove(les);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Les>> GetAll()
        {
            return await _dataContext.Lessen
                .Include(l => l.Leeruitkomsten)
                .Include(l => l.Lesmaterialen)
                .Include(l => l.Planningen)
                .ToListAsync();
        }

        public async Task<Les?> GetById(int id)
        {
            return await _dataContext.Lessen
                .Where(l => l.Id == id)
                .Include(l => l.Leeruitkomsten)
                .Include(l => l.Planningen)
                .Include(l => l.Lesmaterialen)
                .ThenInclude(l => l.LesmateriaalInhoud)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Les entity)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (les != null && entity != null)
            {
                les.Omschrijving = entity.Omschrijving;

                _dataContext.Lessen.Update(les);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task KoppelLeeruitkomstAanLes(int id, Les entity)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Leeruitkomsten).FirstOrDefaultAsync();
            if (les != null && entity != null)
            {
                var leeruitkomstenIds = entity.Leeruitkomsten.Select(l => l.Id).ToList();
                var leeruitkomsten = await _dataContext.Leeruitkomsten.Where(l => leeruitkomstenIds.Contains(l.Id)).ToListAsync();
                les.Leeruitkomsten.Clear();
                les.Leeruitkomsten.AddRange(leeruitkomsten);

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task KoppelLesmateriaalAanLes(int id, Les entity)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Lesmaterialen).FirstOrDefaultAsync();
            if (les != null && entity != null)
            {
                var lesmateriaalIds = entity.Lesmaterialen.Select(l => l.Id).ToList();
                var lesmaterialen = await _dataContext.Lesmaterialen.Where(l => lesmateriaalIds.Contains(l.Id)).ToListAsync();
                les.Lesmaterialen.Clear();
                les.Lesmaterialen.AddRange(lesmaterialen);

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task InplannenLes(int id, Les entity)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Planningen).FirstOrDefaultAsync();
            if (les != null && entity != null)
            {
                les.Planningen.Add(entity.Planningen.FirstOrDefault());

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task OntkoppelLeeruitkomstVanLes(int id, int leeruitkomstId)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Leeruitkomsten).FirstOrDefaultAsync();
            var leeruitkomst = await _dataContext.Leeruitkomsten.Where(l => l.Id == leeruitkomstId).FirstOrDefaultAsync();
            if (les != null && leeruitkomst != null)
            {
                les.Leeruitkomsten.Remove(leeruitkomst);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task VerwijderPlanningVanLes(int id, int planningId)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Planningen).FirstOrDefaultAsync();
            var planning = await _dataContext.Planningen.Where(l => l.Id == planningId).FirstOrDefaultAsync();
            if (les != null && planning != null)
            {
                les.Planningen.Remove(planning);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task OntkoppelLesmateriaalVanLes(int id, int lesmateriaalId)
        {
            var les = await _dataContext.Lessen.Where(l => l.Id == id).Include(l => l.Lesmaterialen).FirstOrDefaultAsync();
            var lesmateriaal = await _dataContext.Lesmaterialen.Where(l => l.Id == lesmateriaalId).FirstOrDefaultAsync();
            if (les != null && lesmateriaal != null)
            {
                les.Lesmaterialen.Remove(lesmateriaal);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
