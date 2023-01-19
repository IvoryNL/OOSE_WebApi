using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Opleidingsprofiel
    {
        public int Id { get; set; }

        public int OpleidingId { get; set; }

        [MaxLength(50)]
        public string Profielnaam { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Opleiding Opleiding { get; set; }
    }
}
