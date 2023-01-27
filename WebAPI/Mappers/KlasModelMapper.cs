using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;

namespace WebAPI.Mappers
{
    public class KlasModelMapper : IDtoMapper<Klas, KlasModelDto>
    {
        private readonly IMapper<Gebruiker, VolledigeGebruikerModelDto> _volledigeGebruikerMapper;

        public KlasModelMapper(IMapper<Gebruiker, VolledigeGebruikerModelDto> volledigeGebruikerMapper)
        {
            _volledigeGebruikerMapper = volledigeGebruikerMapper;
        }

        public KlasModelDto MapToDtoModel(Klas entityModel)
        {
            var klasModelDto = new KlasModelDto();
            klasModelDto.Id = entityModel.Id;   
            klasModelDto.Klasnaam = entityModel.Klasnaam;
            klasModelDto.Gebruikers = entityModel.Gebruikers.Select(g => _volledigeGebruikerMapper.MapToDtoModel(g)).ToList();

            return klasModelDto;
        }
    }
}
