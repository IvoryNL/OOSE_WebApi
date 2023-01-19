using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class IngelogdeGebruikerModelDto : GebruikerModelMetRolDto
    {
        public string JwtToken { get; set; }
    }
}
