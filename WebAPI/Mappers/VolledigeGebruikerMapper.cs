using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class VolledigeGebruikerMapper : IMapper<Gebruiker, VolledigeGebruikerModelDto>
    {
        public VolledigeGebruikerModelDto MapToDtoModel(Gebruiker entityModel)
        {
            var volledigeGebruikerModelDto = new VolledigeGebruikerModelDto();
            volledigeGebruikerModelDto.Id = entityModel.Id;
            volledigeGebruikerModelDto.RolId = entityModel.RolId;
            volledigeGebruikerModelDto.OpleidingId = entityModel.OpleidingId;
            volledigeGebruikerModelDto.OpleidingsprofielId = entityModel.OpleidingsprofielId;
            volledigeGebruikerModelDto.Voornaam = entityModel.Voornaam;
            volledigeGebruikerModelDto.Achternaam = entityModel.Achternaam;
            volledigeGebruikerModelDto.Email = entityModel.Email;
            volledigeGebruikerModelDto.Code = entityModel.Code;
            volledigeGebruikerModelDto.Rol = entityModel.Rol;
            volledigeGebruikerModelDto.Beoordelingsmodellen = entityModel.Beoordelingsmodellen;
            volledigeGebruikerModelDto.TentamensVanStudent = entityModel.TentamensVanStudent;
            volledigeGebruikerModelDto.Klassen = entityModel.Klassen;
            volledigeGebruikerModelDto.Opleiding = entityModel.Opleiding;
            volledigeGebruikerModelDto.Opleidingsprofiel = entityModel.Opleidingsprofiel;

            return volledigeGebruikerModelDto;
        }

        public Gebruiker MapToEntityModel(VolledigeGebruikerModelDto dtoModel)
        {
            var gebruiker = new Gebruiker();
            gebruiker.Id = dtoModel.Id;
            gebruiker.RolId = dtoModel.RolId;
            gebruiker.OpleidingId = dtoModel.OpleidingId;
            gebruiker.OpleidingsprofielId = dtoModel.OpleidingsprofielId;
            gebruiker.Voornaam = dtoModel.Voornaam;
            gebruiker.Achternaam = dtoModel.Achternaam;
            gebruiker.Email = dtoModel.Email;
            gebruiker.Code = dtoModel.Code;
            gebruiker.Beoordelingsmodellen = dtoModel.Beoordelingsmodellen;
            gebruiker.Klassen = dtoModel.Klassen;
            gebruiker.Opleiding = dtoModel.Opleiding;
            gebruiker.Opleidingsprofiel = dtoModel.Opleidingsprofiel;

            return gebruiker;
        }
    }
}
