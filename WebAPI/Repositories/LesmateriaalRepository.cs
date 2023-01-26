using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesmateriaalRepository : IRepository<Lesmateriaal>
    {
        private readonly DataContext _dataContext;

        public LesmateriaalRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Lesmateriaal entity)
        {
            if (entity != null)
            {
                var isExistingLesmateriaal = await IsExsisting(entity);
                if (isExistingLesmateriaal)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Lesmaterialen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var lesmateriaal = await _dataContext.Lesmaterialen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaal != null)
            {
                _dataContext.Lesmaterialen.Remove(lesmateriaal);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Lesmateriaal>> GetAll()
        {
            return await _dataContext.Lesmaterialen
                .Include(l => l.Auteur)
                .Include(l => l.LesmateriaalType)
                .Include(l => l.LesmateriaalInhoud)
                .ToListAsync();
        }

        public async Task<Lesmateriaal?> GetById(int id)
        {
            return await _dataContext.Lesmaterialen
                .Where(l => l.Id == id)
                .Include(l => l.Auteur)
                .Include(l => l.LesmateriaalType)
                .Include(l => l.LesmateriaalInhoud)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Lesmateriaal entity)
        {
            var lesmateriaal = await _dataContext.Lesmaterialen.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaal != null && entity != null)
            {
                var isExistingLesmateriaal = await IsExsisting(id, entity);
                if (isExistingLesmateriaal)
                {
                    ThrowHttpRequestException();
                }

                lesmateriaal.AuteurId = entity.AuteurId;
                lesmateriaal.LesmateriaaltypeId = entity.LesmateriaaltypeId;
                lesmateriaal.Naam = entity.Naam;
                lesmateriaal.Omschrijving = entity.Omschrijving;
                lesmateriaal.Verplicht = entity.Verplicht;

                _dataContext.Lesmaterialen.Update(lesmateriaal);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Lesmateriaal entity)
        {
            var lesmaterialen = await GetAll();
            return lesmaterialen.Any(l => l.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Lesmateriaal entity)
        {
            var lesmaterialen = await GetAll();
            return lesmaterialen.Any(l => l.Naam == entity.Naam && l.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Lesmateriaal met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
