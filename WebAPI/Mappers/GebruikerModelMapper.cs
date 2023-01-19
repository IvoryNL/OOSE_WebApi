using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class GebruikerModelMapper : IEntityMapper<Gebruiker, GebruikerModelDto>
    {
        public Gebruiker MapToEntityModel(GebruikerModelDto dtoModel)
        {
            var user = new Gebruiker();
            user.Voornaam = dtoModel.Voornaam;
            user.Achternaam = dtoModel.Achternaam;
            user.Email = dtoModel.Email;
            user.RolId = dtoModel.RolId;
            user.Code = dtoModel.Code;

            return user;
        }
    }
}
