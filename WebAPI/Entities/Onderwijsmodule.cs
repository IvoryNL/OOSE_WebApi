using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Onderwijsmodule
    {
        public int Id { get; set; }

        public int OpleidingId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        [MaxLength(150)]
        public string Coordinator { get; set; }

        public int Studiepunten { get; set; }

        [MaxLength(30)]
        public string Fase { get; set; }

        [MaxLength(150)]
        public string Ingangseisen { get; set; }

        public int Leerjaar { get; set; }

        public decimal Versie { get; set; }

        public Opleiding? Opleiding { get; set; }

        public List<Onderwijseenheid>? Onderwijseenheden { get; set; }

        public List<Gebruiker>? Docenten { get; set; }

        public List<Onderwijsuitvoering>? Onderwijsuitvoeringen { get; set; }
    }
}
