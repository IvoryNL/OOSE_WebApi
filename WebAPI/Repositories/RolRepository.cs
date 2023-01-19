using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class RolRepository : IRepository<Rol>
    {
        private readonly DataContext _dataContext;

        public RolRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task Create(Rol entity)
        {
            if (entity != null)
            {
                var isExistingRol = await IsExsisting(entity);
                if (isExistingRol)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Rollen.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var rol = await _dataContext.Rollen.FirstOrDefaultAsync(r => r.Id == id);
            if (rol != null) {
                _dataContext.Rollen.Remove(rol);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Rol>> GetAll()
        {
            return await _dataContext.Rollen.ToListAsync();
        }

        public async Task<Rol?> GetById(int id)
        {
            return await _dataContext.Rollen.Where(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(int id, Rol entity)
        {
            var rol = await _dataContext.Rollen.FirstOrDefaultAsync(r => r.Id == id);
            if (rol != null && entity != null)
            {
                var isExistingRol = await IsExsisting(id, entity);
                if (isExistingRol)
                {
                    ThrowHttpRequestException();
                }

                rol.Naam = entity.Naam;

                _dataContext.Rollen.Update(rol);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Rol entity)
        {
            var rollen = await GetAll();
            return rollen.Any(r => r.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Rol entity)
        {
            var rollen = await GetAll();
            return rollen.Any(r => r.Naam == entity.Naam && r.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Rol met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
