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
            var gebruiker = await _dataContext.Gebruikers.FirstOrDefaultAsync(g => g.Id == id);
            if (gebruiker != null)
            {
                _dataContext.Gebruikers.Remove(gebruiker);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Gebruiker>> GetAll()
        {
            return await _dataContext.Gebruikers
                .Include(g => g.Klassen)
                .Include(g => g.Rol)
                .ToListAsync();
        }

        public async Task<Gebruiker?> GetById(int id)
        {
            return await _dataContext.Gebruikers
                .Where(g => g.Id == id)
                .Include(g => g.Rol)
                .Include(g => g.Klassen)
                .Include(g => g.TentamensVanStudent)
                .Include(g => g.Beoordelingsmodellen)
                .Include(g => g.Toetsinschrijvingen)
                .FirstOrDefaultAsync();
        }

        public async Task<Gebruiker?> GetGebruikerByEmail(string email)
        {
            return await _dataContext.Gebruikers
                .Where(g => g.Email == email)
                .Include(g => g.Rol)
                .Include(g => g.Opleiding)
                .Include(g => g.Opleidingsprofiel)
                .Include(g => g.Toetsinschrijvingen)
                .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Gebruiker entity)
        {
            var gebruiker = await _dataContext.Gebruikers.FirstOrDefaultAsync(g => g.Id == id);
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
                var result = await _dataContext.SaveChangesAsync();
            }
        }

        public async Task KoppelStudentAanKlas(int id, Gebruiker entity)
        {
            var gebruiker = await _dataContext.Gebruikers.Include(g => g.Klassen).FirstOrDefaultAsync(g => g.Id == entity.Id);
            if (gebruiker != null && entity != null)
            {
                var klasIds = entity.Klassen!.Select(k => k.Id).ToList();
                var klassen = await _dataContext.Klassen.Where(k => klasIds.Contains(k.Id)).ToListAsync();
                gebruiker.Klassen.Clear();
                gebruiker.Klassen.AddRange(klassen);

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
