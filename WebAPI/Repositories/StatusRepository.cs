using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class StatusRepository : IRepository<Status>
    {
        private readonly DataContext _dataContext;

        public StatusRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Status entity)
        {
            if (entity != null)
            {
                var isExistingStatus = await IsExsisting(entity);
                if (isExistingStatus)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Statussen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var status = await _dataContext.Statussen.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (status != null)
            {
                _dataContext.Statussen.Remove(status);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Status>> GetAll()
        {
            return await _dataContext.Statussen.ToListAsync();
        }

        public async Task<Status?> GetById(int id)
        {
            return await _dataContext.Statussen.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(int id, Status entity)
        {
            var status = await _dataContext.Statussen.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (status != null && entity != null)
            {
                var isExistingStatus = await IsExsisting(id, entity);
                if (isExistingStatus)
                {
                    ThrowHttpRequestException();
                }

                status.Naam = entity.Naam;

                _dataContext.Statussen.Update(status);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Status entity)
        {
            var statussen = await GetAll();
            return statussen.Any(s => s.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Status entity)
        {
            var statussen = await GetAll();
            return statussen.Any(s => s.Naam == entity.Naam && s.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Status met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
