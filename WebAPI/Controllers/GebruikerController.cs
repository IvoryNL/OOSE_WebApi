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
        private readonly IGebruikerRepository<Gebruiker> _userRepository;
        private readonly IEntityMapper<Gebruiker, GebruikerModelDto> _userModelMapper;
        private readonly IDtoMapper<Gebruiker, GebruikerModelMetRolDto> _userModelWithRoleMapper;

        public GebruikerController(
            IGebruikerRepository<Gebruiker> userRepository,
            IEntityMapper<Gebruiker, GebruikerModelDto> userMapper,
            IDtoMapper<Gebruiker, GebruikerModelMetRolDto> userModelWithRoleMapper)
        {
            _userRepository = userRepository;
            _userModelMapper = userMapper;
            _userModelWithRoleMapper = userModelWithRoleMapper;
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GebruikerModelMetRolDto>>> Get()
        {
            var result = await _userRepository.GetAll();
            if (result != null)
            {
                var userModelWithRoleDto = result.Select(_userModelWithRoleMapper.MapToDtoModel);
                return Ok(userModelWithRoleDto);
            }

            return Ok();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GebruikerModelMetRolDto>> Get(int id)
        {
            var result = await _userRepository.GetById(id);
            if (result != null)
            {
                var userModelWithRoleDto = _userModelWithRoleMapper.MapToDtoModel(result);
                return Ok(userModelWithRoleDto);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] GebruikerModelDto userModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var password = AuthenticationHelper.CreatePasswordHash(userModelDto.Password);
            var user = _userModelMapper.MapToEntityModel(userModelDto);
            user.PasswordHash = password.PasswordHash;
            user.PasswordSalt = password.PasswordSalt;            

            try
            {
                await _userRepository.Create(user);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }
        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GebruikerModelDto userModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var password = AuthenticationHelper.CreatePasswordHash(userModelDto.Password);
            var user = _userModelMapper.MapToEntityModel(userModelDto);
            user.PasswordHash = password.PasswordHash;
            user.PasswordSalt = password.PasswordSalt;

            await _userRepository.Update(id, user);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.Delete(id);
            return Ok();
        }
    }
}
