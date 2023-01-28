using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class TentamenRepository : ITentamenRepository<Tentamen>
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
                .Include(t => t.Planningen)
                .ToListAsync();
        }

        public async Task<Tentamen?> GetById(int id)
        {
            return await _dataContext.Tentamens
                .Where(t => t.Id == id)
                .Include(t => t.Vorm)
                .Include(t => t.Beoordelingsmodel)
                .Include(t => t.Leeruitkomsten)
                .Include(t => t.Planningen)
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

        public async Task<List<Tentamen>> GetAllTentamensVanOnderwijsuitvoeringStudent(int id)
        {
            var student = await _dataContext.Gebruikers
                .Where(g => g.Id == id)
                .Include(g => g.Klassen)
                .ThenInclude(k => k.Onderwijsuitvoeringen)
                .FirstOrDefaultAsync();
            var onderwijsuitvoeringen = student.Klassen
                .SelectMany(k => k.Onderwijsuitvoeringen);
            var huidigeOnderwijsuitvoering = onderwijsuitvoeringen
                .OrderByDescending(o => o.Jaartal)
                .ThenByDescending(o => o.Periode)
                .FirstOrDefault();

            return await _dataContext.Planningen
                .Where(p => p.OnderwijsuitvoeringId == huidigeOnderwijsuitvoering.Id && p.Tentamens.Any())
                .Include(p => p.Tentamens)
                .Select(p => new Tentamen 
                { 
                    Id = p.Tentamens.FirstOrDefault().Id,
                    Naam = p.Tentamens.FirstOrDefault().Naam,
                    Planningen = new List<Planning> { p }
                })
                .ToListAsync();
        }

        public async Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodel()
        {
            return await _dataContext.Tentamens
                .Where(t => t.Beoordelingsmodel == null)
                .ToListAsync();
        }

        public async Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodelVoorWijziging(int beoordelingsmodelId)
        {
            return await _dataContext.Tentamens
                .Where(t => t.Beoordelingsmodel == null || t.Beoordelingsmodel.Id == beoordelingsmodelId)
                .ToListAsync();
        }

        public async Task OntkoppelLeeruitkomstVanTentamen(int id, int leeruitkomstId)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).Include(t => t.Leeruitkomsten).FirstOrDefaultAsync();
            var leeruitkomst = await _dataContext.Leeruitkomsten.Where(l => l.Id == leeruitkomstId).FirstOrDefaultAsync();
            if (tentamen != null && leeruitkomst != null)
            {
                tentamen.Leeruitkomsten!.Remove(leeruitkomst);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task VerwijderPlanningVanTentamen(int id, int planningId)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).Include(t => t.Planningen).FirstOrDefaultAsync();
            var planning = await _dataContext.Planningen.Where(l => l.Id == planningId).FirstOrDefaultAsync();
            if (tentamen != null && planning != null)
            {
                var toetsInschrijvingen = await _dataContext.Toetsinschrijvingen.Where(t => t.PlanningId == planning.Id).ToListAsync();
                tentamen.Planningen!.Remove(planning);
                _dataContext.Toetsinschrijvingen.RemoveRange(toetsInschrijvingen);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task KoppelLeeruitkomstAanTentamen(int id, Tentamen entity)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).Include(t => t.Leeruitkomsten).FirstOrDefaultAsync();
            if (tentamen != null && entity != null)
            {
                var leeruitkomstenIds = entity.Leeruitkomsten!.Select(l => l.Id).ToList();
                var leeruitkomsten = await _dataContext.Leeruitkomsten.Where(l => leeruitkomstenIds.Contains(l.Id)).ToListAsync();
                tentamen.Leeruitkomsten!.Clear();
                tentamen.Leeruitkomsten.AddRange(leeruitkomsten);

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task InplannenTentamen(int id, Tentamen entity)
        {
            var tentamen = await _dataContext.Tentamens.Where(t => t.Id == id).Include(l => l.Planningen).FirstOrDefaultAsync();
            if (tentamen != null && entity != null)
            {
                tentamen.Planningen!.Add(entity.Planningen!.FirstOrDefault());

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
