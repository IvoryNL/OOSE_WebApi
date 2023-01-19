using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class ToetsinschrijvingRepository : IRepository<Toetsinschrijving>
    {
        private readonly DataContext _dataContext;

        public ToetsinschrijvingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Toetsinschrijving entity)
        {
            if (entity != null)
            {
                var isExistingToetsinschrijving = await IsExsisting(entity);
                if (isExistingToetsinschrijving)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Toetsinschrijvingen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var toetsinschrijving = await _dataContext.Toetsinschrijvingen.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (toetsinschrijving != null)
            {
                _dataContext.Toetsinschrijvingen.Remove(toetsinschrijving);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Toetsinschrijving>> GetAll()
        {
            return await _dataContext.Toetsinschrijvingen
                .Include(t => t.Student)
                .Include(t => t.Tentamen)
                .Include(t => t.Planning)
                .ToListAsync();
        }

        public async Task<Toetsinschrijving?> GetById(int id)
        {
            return await _dataContext.Toetsinschrijvingen
                .Where(t => t.Id == id)
                .Include(t => t.Student)
                .Include(t => t.Tentamen)
                .Include(t => t.Planning)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Toetsinschrijving entity)
        {
            var toetsinschrijving = await _dataContext.Toetsinschrijvingen.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (toetsinschrijving != null && entity != null)
            {
                var isExistingToetsinschrijving = await IsExsisting(id, entity);
                if (isExistingToetsinschrijving)
                {
                    ThrowHttpRequestException();
                }

                toetsinschrijving.StudentId = entity.StudentId;
                toetsinschrijving.TentamenId = entity.TentamenId;
                toetsinschrijving.PlanningId = entity.PlanningId;

                _dataContext.Toetsinschrijvingen.Update(toetsinschrijving);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Toetsinschrijving entity)
        {
            var toetsinschrijvingen = await GetAll();
            return toetsinschrijvingen.Any(t => t.StudentId == entity.StudentId && t.TentamenId == entity.TentamenId && t.PlanningId == entity.PlanningId);
        }

        private async Task<bool> IsExsisting(int id, Toetsinschrijving entity)
        {
            var toetsinschrijvingen = await GetAll();
            return toetsinschrijvingen.Any(t => t.StudentId == entity.StudentId && t.TentamenId == entity.TentamenId && t.PlanningId == entity.PlanningId && t.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Toetsinschrijving met deze student, tentamen en planning.";
            throw new HttpRequestException(message);
        }
    }
}
