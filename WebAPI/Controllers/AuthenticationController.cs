using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models;
using WebAPI.Models.Dto;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IGebruikerRepository<Gebruiker> _userRepository;
        private readonly IDtoMapper<Gebruiker, IngelogdeGebruikerModelDto> _loggedInUserDtoMapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration, IGebruikerRepository<Gebruiker> userRepository, IDtoMapper<Gebruiker, IngelogdeGebruikerModelDto> loggedInUserDtoMapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _loggedInUserDtoMapper = loggedInUserDtoMapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<IngelogdeGebruikerModelDto>> Login(LoginModelDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetGebruikerByEmail(loginDto.Email);
            if (user == null)
            {
                var message = "Het opgegeven e-mailadres bestaat niet.";
                return new NotFoundObjectResult(message);
            }

            var hashedPassword = new HashedPasswordModel
            {
                PasswordHash= user.PasswordHash,
                PasswordSalt= user.PasswordSalt
            };
            if (!AuthenticationHelper.VerifyPasswordHash(loginDto.Password, hashedPassword))
            {
                var message = "Het wachtwoord is onjuist";
                return new ConflictObjectResult(message);
            }

            var jwtToken = AuthenticationHelper.CreateToken(user, _configuration.GetSection("AppSettings:Token").Value!);
            var loggedInUserModelDto = _loggedInUserDtoMapper.MapToDtoModel(user);
            loggedInUserModelDto.JwtToken = jwtToken;

            return Ok(loggedInUserModelDto);
        }
    }
}
