using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class CreateGebruikerModelMapper : IEntityMapper<Gebruiker, CreateGebruikerModelDto>
    {
        public Gebruiker MapToEntityModel(CreateGebruikerModelDto dtoModel)
        {
            var user = new Gebruiker();
            user.Id = dtoModel.Id;
            user.Voornaam = dtoModel.Voornaam;
            user.Achternaam = dtoModel.Achternaam;
            user.Email = dtoModel.Email;
            user.RolId = dtoModel.RolId;
            user.Code = dtoModel.Code;

            return user;
        }
    }
}
