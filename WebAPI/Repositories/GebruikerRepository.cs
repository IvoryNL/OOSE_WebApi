using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class GebruikerRepository : IGebruikerRepository<Gebruiker>
    {
        private readonly DataContext _dataContext;

        public GebruikerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Gebruiker entity)
        {
            if (entity != null)
            {
                var isExistingEmail = await IsExsisting(entity);
                if (isExistingEmail)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Gebruikers.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var gebruiker = await _dataContext.Gebruikers.FirstOrDefaultAsync(u => u.Id == id);
            if (gebruiker != null)
            {
                _dataContext.Gebruikers.Remove(gebruiker);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Gebruiker>> GetAll()
        {
            return await _dataContext.Gebruikers
                .Include(u => u.Rol)
                .Include(u => u.Opleiding)
                .Include(u => u.Opleidingsprofiel)
                .ToListAsync();
        }

        public async Task<Gebruiker?> GetById(int id)
        {
            return await _dataContext.Gebruikers
                .Where(u => u.Id == id)
                .Include(u => u.Rol)
                .Include(u => u.Opleiding)
                .Include(u => u.Opleidingsprofiel)
                .FirstOrDefaultAsync();
        }

        public async Task<Gebruiker?> GetUserByEmail(string email)
        {
            return await _dataContext.Gebruikers
                .Where(u => u.Email == email)
                .Include(u => u.Rol)
                .Include(u => u.Opleiding)
                .Include(u => u.Opleidingsprofiel)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Gebruiker entity)
        {
            var gebruiker = await _dataContext.Gebruikers.FirstOrDefaultAsync(u => u.Id == entity.Id);
            if (gebruiker != null && entity != null)
            {
                var isExistingEmail = await IsExsisting(id, entity);
                if (isExistingEmail)
                {
                    ThrowHttpRequestException();
                }

                gebruiker.RolId = entity.RolId;
                gebruiker.OpleidingId = entity.OpleidingId;
                gebruiker.OpleidingsprofielId = entity.OpleidingsprofielId;
                gebruiker.Email = entity.Email;
                gebruiker.Code = entity.Code;
                gebruiker.Voornaam = entity.Voornaam;
                gebruiker.Achternaam = entity.Achternaam;

                _dataContext.Gebruikers.Update(gebruiker);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Gebruiker entity)
        {
            var gebruikers = await GetAll();
            return gebruikers.Any(g => g.Email == entity.Email);
        }

        private async Task<bool> IsExsisting(int id, Gebruiker entity)
        {
            var gebruikers = await GetAll();
            return gebruikers.Any(g => g.Email == entity.Email && g.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Gebruiker met dit emailadres.";
            throw new HttpRequestException(message);
        }
    }
}
