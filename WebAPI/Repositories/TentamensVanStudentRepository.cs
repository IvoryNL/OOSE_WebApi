using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class TentamensVanStudentRepository : IRepository<TentamenVanStudent>
    {
        private readonly DataContext _dataContext;

        public TentamensVanStudentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(TentamenVanStudent entity)
        {
            if (entity == null)
            {
                _dataContext.TentamenVanStudenten.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var tentamenUpload = await _dataContext.TentamenVanStudenten.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tentamenUpload != null)
            {
                _dataContext.TentamenVanStudenten.Remove(tentamenUpload);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<TentamenVanStudent>> GetAll()
        {
            return await _dataContext.TentamenVanStudenten
                .Include(t => t.Student)
                .Include(t => t.Beoordeling)
                .ToListAsync();
        }

        public async Task<TentamenVanStudent?> GetById(int id)
        {
            return await _dataContext.TentamenVanStudenten
                .Include(t => t.Student)
                .Include(t => t.Beoordeling)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, TentamenVanStudent entity)
        {
            var tentamenUpload = await _dataContext.TentamenVanStudenten.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (tentamenUpload != null && entity != null)
            {
                tentamenUpload.StudentId = entity.StudentId;
                tentamenUpload.TentamenId = entity.TentamenId;
                tentamenUpload.Bestand = entity.Bestand;
                tentamenUpload.Datum = entity.Datum;

                _dataContext.TentamenVanStudenten.Remove(entity);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
