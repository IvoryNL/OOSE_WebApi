using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class LesmateriaalVorm
    {
        public int Id { get; set; }

        public int LesmateriaalId { get; set; }

        public string Structuur { get; set; }

        [MaxLength(15)]
        public string Bestandstype { get; set; }

        public decimal Versie { get; set; }

        public Lesmateriaal Lesmateriaal { get; set; }
    }
}
