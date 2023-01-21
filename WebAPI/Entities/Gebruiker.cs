using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Gebruiker
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public int? OpleidingId { get; set; }

        public int? OpleidingsprofielId { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(30)]
        public string Voornaam { get; set; }

        [MaxLength(60)]
        public string Achternaam { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public Rol? Rol { get; set; }

        public Opleiding? Opleiding { get; set; }

        public Opleidingsprofiel? Opleidingsprofiel { get; set; }

        public List<Onderwijsmodule>? Onderwijsmodules { get; set; }

        public List<Klas>? Klassen { get; set; }

        //public List<Klas_Gebruiker?> Klas_Gebruiker { get; set; }

        public List<TentamenVanStudent>? TentamensVanStudent { get; set; }

        public List<Beoordeling>? DocentBeoordelingen { get; set; }

        public List<Beoordelingsmodel>? Beoordelingsmodellen { get; set; }

        public List<Onderwijsuitvoering>? Onderwijsuitvoeringen { get; set; }
    }
}
