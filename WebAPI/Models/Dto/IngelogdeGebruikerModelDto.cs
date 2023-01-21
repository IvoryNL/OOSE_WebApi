using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class IngelogdeGebruikerModelDto : BaseGebruikerModelDto
    {
        public string JwtToken { get; set; }
    }
}
