using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class GebruikerModelDto : BaseGebruikerModelDto
    {
        [Required]
        public string Password { get; set; }
    }
}
