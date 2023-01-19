using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class LesmateriaalTypeRepository : IRepository<LesmateriaalType>
    {
        private readonly DataContext _dataContext;

        public LesmateriaalTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(LesmateriaalType entity)
        {
            if (entity != null)
            {
                var isExistingLesmateriaalType = await IsExsisting(entity);
                if (isExistingLesmateriaalType)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.LesmateriaalTypes.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var lesmateriaalType = await _dataContext.LesmateriaalTypes.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalType != null)
            {
                _dataContext.LesmateriaalTypes.Remove(lesmateriaalType);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<LesmateriaalType>> GetAll()
        {
            return await _dataContext.LesmateriaalTypes.ToListAsync();
        }

        public async Task<LesmateriaalType?> GetById(int id)
        {
            return await _dataContext.LesmateriaalTypes.Where(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(int id, LesmateriaalType entity)
        {
            var lesmateriaalType = await _dataContext.LesmateriaalTypes.Where(l => l.Id == id).FirstOrDefaultAsync();
            if (lesmateriaalType != null && entity != null)
            {
                var isExistingLesmateriaalType = await IsExsisting(id, entity);
                if (isExistingLesmateriaalType)
                {
                    ThrowHttpRequestException();
                }

                lesmateriaalType.Naam = entity.Naam;

                _dataContext.LesmateriaalTypes.Update(lesmateriaalType);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(LesmateriaalType entity)
        {
            var lesmateriaaltypen = await GetAll();
            return lesmateriaaltypen.Any(l => l.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, LesmateriaalType entity)
        {
            var lesmateriaaltypen = await GetAll();
            return lesmateriaaltypen.Any(l => l.Naam == entity.Naam && l.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een LesmateriaalType met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
