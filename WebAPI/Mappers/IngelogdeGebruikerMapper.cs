using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class IngelogdeGebruikerMapper : IDtoMapper<Gebruiker, IngelogdeGebruikerModelDto>
    {
        public IngelogdeGebruikerModelDto MapToDtoModel(Gebruiker entityModel)
        {
            var loggedInUserModelDto = new IngelogdeGebruikerModelDto();
            loggedInUserModelDto.Id = entityModel.Id;
            loggedInUserModelDto.Voornaam = entityModel.Voornaam;
            loggedInUserModelDto.Achternaam = entityModel.Achternaam;
            loggedInUserModelDto.Email = entityModel.Email;
            loggedInUserModelDto.Code = entityModel.Code;
            loggedInUserModelDto.RolId = entityModel.RolId;
            loggedInUserModelDto.Rol = entityModel.Rol;

            return loggedInUserModelDto;
        }
    }
}
