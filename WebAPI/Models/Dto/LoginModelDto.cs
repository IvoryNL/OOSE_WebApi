using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Dto
{
    public class LoginModelDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Wachtwoord { get; set; }
    }
}
