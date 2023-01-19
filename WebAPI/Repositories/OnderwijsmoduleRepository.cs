﻿using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class OnderwijsmoduleRepository : IOnderwijsmoduleRepository<Onderwijsmodule>
    {
        private readonly DataContext _dataContext;

        public OnderwijsmoduleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Onderwijsmodule entity)
        {
            if (entity != null)
            {
                var isExistingOnderwijsmodule = await IsExsisting(entity);
                if (isExistingOnderwijsmodule)
                {
                    ThrowHttpRequestException();
                }

                _dataContext.Onderwijsmodules.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var onderwijsmodule = await _dataContext.Onderwijsmodules.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijsmodule != null)
            {
                _dataContext.Onderwijsmodules.Remove(onderwijsmodule);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<List<Onderwijsmodule>> GetAll()
        {
            return await _dataContext.Onderwijsmodules
                .Include(o => o.Opleiding)
                .Include(o => o.Status)
                .Include(o => o.Onderwijseenheden)
                .Include(o => o.Docenten)
                .ToListAsync();
        }

        public async Task<Onderwijsmodule?> GetById(int id)
        {
            return await _dataContext.Onderwijsmodules
                .Where(o => o.Id == id)
                .Include(o => o.Opleiding)
                .Include(o => o.Status)
                .Include(o => o.Onderwijseenheden)
                .Include(o => o.Docenten)
                .FirstOrDefaultAsync();
        }

        public async Task<Onderwijsmodule?> GetWithRelationshipsById(int id)
        {
            return await _dataContext.Onderwijsmodules
                .Where(o => o.Id == id)
                .Include(o => o.Onderwijseenheden)
                .ThenInclude(o => o.Leerdoelen)
                .ThenInclude(o => o.Leeruitkomsten)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IncreaseVersion(int id)
        {
            var onderwijsmodule = await _dataContext.Onderwijsmodules.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijsmodule != null)
            {
                onderwijsmodule.Versie += 0.01m;

                _dataContext.Onderwijsmodules.Update(onderwijsmodule);
                await _dataContext.SaveChangesAsync();
            }

            return true;
        }

        public async Task Update(int id, Onderwijsmodule entity)
        {
            var onderwijsmodule = await _dataContext.Onderwijsmodules.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (onderwijsmodule != null && entity != null)
            {
                var isExistingOnderwijsmodule = await IsExsisting(id, entity);
                if (isExistingOnderwijsmodule)
                {
                    ThrowHttpRequestException();
                }

                onderwijsmodule.OpleidingId = entity.OpleidingId;
                onderwijsmodule.StatusId = entity.StatusId;
                onderwijsmodule.Naam = entity.Naam;
                onderwijsmodule.Beschrijving = entity.Beschrijving;
                onderwijsmodule.Coordinator = entity.Coordinator;
                onderwijsmodule.Studiepunten = entity.Studiepunten;
                onderwijsmodule.Fase = entity.Fase;
                onderwijsmodule.Ingangseisen = entity.Ingangseisen;
                onderwijsmodule.Leerjaar = entity.Leerjaar;
                onderwijsmodule.Versie += 0.01m;

                _dataContext.Onderwijsmodules.Update(onderwijsmodule);
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExsisting(Onderwijsmodule entity)
        {
            var onderwijsmodules = await GetAll();
            return onderwijsmodules.Any(o => o.Naam == entity.Naam);
        }

        private async Task<bool> IsExsisting(int id, Onderwijsmodule entity)
        {
            var onderwijsmodules = await GetAll();
            return onderwijsmodules.Any(o => o.Naam == entity.Naam && o.Id != id);
        }

        private void ThrowHttpRequestException()
        {
            var message = "Er bestaat al een Onderwijsmodule met deze naam.";
            throw new HttpRequestException(message);
        }
    }
}