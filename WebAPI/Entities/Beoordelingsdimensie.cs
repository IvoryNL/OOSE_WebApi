using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Beoordelingsdimensie
    {
        public int Id { get; set; }

        public int BeoordelingscriteriaId { get; set; }

        [MaxLength(50)]
        public string Titel { get; set; }

        [MaxLength(150)]
        public string Omschrijving { get; set; }

        [MaxLength(150)]
        public string Toelichting { get; set; }

        public decimal Cijferwaarde { get; set; }

        public Beoordelingscriteria? Beoordelingscriteria { get; set; }
    }
}
