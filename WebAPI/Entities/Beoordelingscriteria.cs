using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Beoordelingscriteria
    {
        public int Id { get; set; }

        public int BeoordelingsonderdeelId { get; set; }

        public int? LeeruitkomstId { get; set; }

        [MaxLength(100)]
        public string Criteria { get; set; }

        [MaxLength(150)]
        public string Omschrijving { get; set; }

        public decimal Resultaat { get; set; }

        public decimal Gewicht { get; set; }

        public decimal Grens { get; set; }

        public bool Verplicht { get; set; }

        public Beoordelingsonderdeel? Beoordelingsonderdeel { get; set; }

        public Leeruitkomst? Leeruitkomst { get; set; }

        public List<Beoordelingsdimensie>? Beoordelingsdimensies { get; set; }
    }
}
