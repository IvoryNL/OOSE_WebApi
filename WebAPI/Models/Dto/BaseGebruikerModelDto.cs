using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class BaseGebruikerModelDto
    {
        public int Id { get; set; }

        [Required]
        public string Voornaam { get; set; }

        [Required]
        public string Achternaam { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int RolId { get; set; }
    }
}
