using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Onderwijseenheid
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        [MaxLength(150)]
        public string Coordinator { get; set; }

        public decimal Studiepunten { get; set; }

        public List<Onderwijsmodule>? Onderwijsmodules { get; set; }

        public List<Tentamen>? Tentamens { get; set; }

        public List<Leerdoel>? Leerdoelen { get; set; }
    }
}
