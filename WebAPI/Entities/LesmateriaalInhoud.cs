using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class LesmateriaalInhoud
    {
        public int Id { get; set; }

        public int LesmateriaalId { get; set; }

        public string Inhoud { get; set; }

        public decimal Versie { get; set; }

        public Lesmateriaal? Lesmateriaal { get; set; }
    }
}
