using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class OnderwijsuitvoeringRepository : IRepository<Onderwijsuitvoering>
    {
        private readonly DataContext _dataContext;

        public OnderwijsuitvoeringRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Onderwijsuitvoering entity)
        {
            if (entity != null)
            {
                var isExistingOnderwijsuitvoering = await IsExsisting(entity);
                if (isExistingOnderwijsuitvoering)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Onderwijsuitvoeringen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var onderwijsuitvoering = await _dataContext.Onderwijsuitvoeringen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijsuitvoering != null)
            {
                _dataContext.Onderwijsuitvoeringen.Remove(onderwijsuitvoering);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Onderwijsuitvoering>> GetAll()
        {
            return await _dataContext.Onderwijsuitvoeringen
                .Include(o => o.Onderwijsmodule)
                .Include(o => o.Docent)
                .Include(o => o.Klassen)
                .Include(o => o.Planningen)
                .ToListAsync();
        }

        public async Task<Onderwijsuitvoering?> GetById(int id)
        {
            return await _dataContext.Onderwijsuitvoeringen
                .Where(o => o.Id == id)
                .Include(o => o.Onderwijsmodule)
                .Include(o => o.Docent)
                .Include(o => o.Klassen)
                .Include(o => o.Planningen)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Onderwijsuitvoering entity)
        {
            var onderwijsuitvoering = await _dataContext.Onderwijsuitvoeringen.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijsuitvoering != null && entity != null)
            {
                var isExistingOnderwijsuitvoering = await IsExsisting(id, entity);
                if (isExistingOnderwijsuitvoering)
                {
                    ThrowHttpRequestException();
                }

                onderwijsuitvoering.OnderwijsmoduleId = entity.OnderwijsmoduleId;
                onderwijsuitvoering.DocentId = entity.DocentId;
                onderwijsuitvoering.Jaartal = entity.Jaartal;
                onderwijsuitvoering.Periode = entity.Periode;

                _dataContext.Onderwijsuitvoeringen.Update(onderwijsuitvoering);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Onderwijsuitvoering entity)
        {
            var onderwijsuitvoeringen= await GetAll();
            return onderwijsuitvoeringen.Any(o => o.Jaartal == entity.Jaartal && o.Periode == entity.Periode);
        }

        private async Task<bool> IsExsisting(int id, Onderwijsuitvoering entity)
        {
            var onderwijsuitvoeringen = await GetAll();
            return onderwijsuitvoeringen.Any(o => o.Jaartal == entity.Jaartal && o.Periode == entity.Periode && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Onderwijsuitvoering met deze periode en jaartal.";
            throw new HttpRequestException(message);
        }
    }
}
