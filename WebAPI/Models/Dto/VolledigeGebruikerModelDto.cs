using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class VolledigeGebruikerModelDto : BaseGebruikerModelDto
    {
        public int? OpleidingId { get; set; }

        public int? OpleidingsprofielId { get; set; }

        public Opleiding? Opleiding { get; set; }

        public Opleidingsprofiel? Opleidingsprofiel { get; set; }

        public List<Klas>? Klassen { get; set; }

        public List<TentamenVanStudent>? TentamensVanStudent { get; set; }

        public List<Beoordelingsmodel>? Beoordelingsmodellen { get; set; }

        public List<Toetsinschrijving>? Toetsinschrijvingen { get; set; }
    }
}
