using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class AuteurRepository : IRepository<Auteur>
    {
        private readonly DataContext _dataContext;

        public AuteurRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Auteur entity)
        {
            if (entity != null)
            {
                var isExistingAuteur = await IsExsisting(entity);
                if (isExistingAuteur)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Auteurs.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var auteur = await _dataContext.Auteurs.FirstOrDefaultAsync(a => a.Id == id);
            if (auteur != null)
            {
                _dataContext.Auteurs.Remove(auteur);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Auteur>> GetAll()
        {
            return await _dataContext.Auteurs.ToListAsync();
        }

        public async Task<Auteur?> GetById(int id)
        {
            return await _dataContext.Auteurs.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Update(int id, Auteur entity)
        {
            var auteur = await _dataContext.Auteurs.FirstOrDefaultAsync(a => a.Id == id);
            if (auteur != null && entity != null)
            {
                var isExistingAuteur = await IsExsisting(id, entity);
                if (isExistingAuteur)
                {
                    ThrowHttpRequestException();
                }

                auteur.Naam = entity.Naam;

                _dataContext.Auteurs.Update(auteur);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Auteur entity)
        {
            var auteurs = await GetAll();
            return auteurs.Any(a => a.Naam  == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Auteur entity)
        {
            var auteurs = await GetAll();
            return auteurs.Any(a => a.Naam == entity.Naam && a.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Auteur met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}
