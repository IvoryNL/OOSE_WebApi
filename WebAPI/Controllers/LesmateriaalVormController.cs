using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LesmateriaalVormController : ControllerBase
    {
        private readonly IRepository<LesmateriaalVorm> _lesmateriaalVormRepository;

        public LesmateriaalVormController(IRepository<LesmateriaalVorm> lesmateriaalVormRepository)
        {
            _lesmateriaalVormRepository = lesmateriaalVormRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LesmateriaalVorm>>> Get()
        {
            var result = await _lesmateriaalVormRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LesmateriaalVorm>> Get(int id)
        {
            var result = await _lesmateriaalVormRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] LesmateriaalVorm lesmateriaalVorm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesmateriaalVormRepository.Create(lesmateriaalVorm);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LesmateriaalVorm lesmateriaalVorm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesmateriaalVormRepository.Update(id, lesmateriaalVorm);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lesmateriaalVormRepository.Delete(id);
            return Ok();
        }
    }
}
