using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Klas
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Klasnaam { get; set; }

        public List<Gebruiker>? Gebruikers { get; set; }

        public List<Onderwijsuitvoering>? Onderwijsuitvoeringen { get; set; }
    }
}
