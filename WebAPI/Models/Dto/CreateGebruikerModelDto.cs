using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class CreateGebruikerModelDto : BaseGebruikerModelDto
    {
        [Required]
        public string Password { get; set; }
    }
}
