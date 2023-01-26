using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class KlasModelDto
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Klasnaam { get; set; }

        public List<VolledigeGebruikerModelDto>? Gebruikers { get; set; }
    }
}
