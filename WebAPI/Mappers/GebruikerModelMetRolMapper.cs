using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class GebruikerModelMetRolMapper : IDtoMapper<Gebruiker, GebruikerModelMetRolDto>
    {
        public GebruikerModelMetRolDto MapToDtoModel(Gebruiker entityModel)
        {
            var userModelWithRoleDto = new GebruikerModelMetRolDto();
            userModelWithRoleDto.Id = entityModel.Id;
            userModelWithRoleDto.Voornaam = entityModel.Voornaam;
            userModelWithRoleDto.Achternaam = entityModel.Achternaam;
            userModelWithRoleDto.Email = entityModel.Email;
            userModelWithRoleDto.Code = entityModel.Code;
            userModelWithRoleDto.RolId = entityModel.RolId;
            userModelWithRoleDto.Rol = entityModel.Rol;

            return userModelWithRoleDto;
        }
    }
}
