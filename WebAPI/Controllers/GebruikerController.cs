using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GebruikerController : ControllerBase
    {
        private readonly IGebruikerRepository<Gebruiker> _gebruikerRepository;
        private readonly IEntityMapper<Gebruiker, CreateGebruikerModelDto> _createGebruikerModelMapper;
        private readonly IMapper<Gebruiker, VolledigeGebruikerModelDto> _volledigeGebruikerModelMapper;

        public GebruikerController(
            IGebruikerRepository<Gebruiker> gebruikerRepository,
            IEntityMapper<Gebruiker, CreateGebruikerModelDto> createGebruikerModelMapper,
            IMapper<Gebruiker, VolledigeGebruikerModelDto> volledigeGebruikerModelMapper)
        {
            _gebruikerRepository = gebruikerRepository;
            _createGebruikerModelMapper = createGebruikerModelMapper;
            _volledigeGebruikerModelMapper = volledigeGebruikerModelMapper;
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<VolledigeGebruikerModelDto>>> Get()
        {
            var result = await _gebruikerRepository.GetAll();
            if (result != null)
            {
                var volledigeGebruikerModelDto = result.Select(_volledigeGebruikerModelMapper.MapToDtoModel);
                return Ok(volledigeGebruikerModelDto);
            }

            return Ok();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<VolledigeGebruikerModelDto>> Get(int id)
        {
            var result = await _gebruikerRepository.GetById(id);
            if (result != null)
            {
                var volledigeGebruikerModelDto = _volledigeGebruikerModelMapper.MapToDtoModel(result);
                return Ok(volledigeGebruikerModelDto);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] CreateGebruikerModelDto createBruikerModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var password = AuthenticationHelper.CreatePasswordHash(createBruikerModelDto.Password);
            var gebruiker = _createGebruikerModelMapper.MapToEntityModel(createBruikerModelDto);
            gebruiker.PasswordHash = password.PasswordHash;
            gebruiker.PasswordSalt = password.PasswordSalt;            

            try
            {
                await _gebruikerRepository.Create(gebruiker);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }
        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VolledigeGebruikerModelDto volledigeGebruikerModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = _volledigeGebruikerModelMapper.MapToEntityModel(volledigeGebruikerModelDto);

            await _gebruikerRepository.Update(id, gebruiker);
            return Ok();
        }

        [HttpGet("GetGebruikerByEmail/{email}")]
        public async Task<IActionResult> GetGebruikerByEmail(string email)
        {
            var result = await _gebruikerRepository.GetGebruikerByEmail(email);
            if (result != null)
            {
                var volledigeGebruikerModelDto = _volledigeGebruikerModelMapper.MapToDtoModel(result);
                return Ok(volledigeGebruikerModelDto);
            }

            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gebruikerRepository.Delete(id);
            return Ok();
        }

        [HttpPut("KoppelStudentAanKlas/{id}")]
        public async Task<IActionResult> KoppelStudentAanKlas(int id, [FromBody] VolledigeGebruikerModelDto volledigeGebruikerModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = _volledigeGebruikerModelMapper.MapToEntityModel(volledigeGebruikerModelDto);

            await _gebruikerRepository.KoppelStudentAanKlas(id, gebruiker);
            return Ok();
        }
    }
}
