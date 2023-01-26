using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebApi.Tests.OnderwijseenheidControllerTest
{
    public class OnderwijseenheidRepositoryTest : IRepository<Onderwijseenheid>
    {
        public List<Onderwijseenheid> Onderwijseenheden { get; set; }

        public OnderwijseenheidRepositoryTest()
        {
            Onderwijseenheden = new List<Onderwijseenheid>();
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 1, Naam = "Onderwijseenheid 1 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 2, Naam = "Onderwijseenheid 2 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 3, Naam = "Onderwijseenheid 3 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 4, Naam = "Onderwijseenheid 4 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
        }

        public async Task Create(Onderwijseenheid entity)
        {
            Onderwijseenheden.Add(entity);
        }

        public async Task Delete(int id)
        {
            var onderwijseenheid = Onderwijseenheden.FirstOrDefault(o => o.Id == id);
            Onderwijseenheden.Remove(onderwijseenheid);
        }

        public async Task<List<Onderwijseenheid>> GetAll()
        {
            return Onderwijseenheden;
        }

        public async Task<Onderwijseenheid?> GetById(int id)
        {
            return Onderwijseenheden.FirstOrDefault(o => o.Id == id);
        }

        public async Task Update(int id, Onderwijseenheid entity)
        {
            var onderwijseenheid = Onderwijseenheden.FirstOrDefault(o => o.Id == id);
            onderwijseenheid.Naam = entity.Naam;
            onderwijseenheid.Studiepunten = 20;
        }
    }
}
