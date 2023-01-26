using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Beoordelingsmodel
    {
        public int Id { get; set; }

        public int TentamenId { get; set; }

        public int DocentId { get; set; }

        public int StatusId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Naam { get; set; }

        public Tentamen? Tentamen { get; set; }

        public Gebruiker? Docent { get; set; }

        public Status? Status { get; set; }

        public List<Beoordelingsonderdeel>? Beoordelingsonderdelen { get; set; }
    }
}
