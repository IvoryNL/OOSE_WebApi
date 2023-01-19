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
    public class LesmateriaalInhoudController : ControllerBase
    {
        private readonly IRepository<LesmateriaalInhoud> _lesmateriaalInhoudRepository;

        public LesmateriaalInhoudController(IRepository<LesmateriaalInhoud> lesmateriaalInhoudRepository)
        {
            _lesmateriaalInhoudRepository = lesmateriaalInhoudRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LesmateriaalInhoud>>> Get()
        {
            var result = await _lesmateriaalInhoudRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LesmateriaalInhoud>> Get(int id)
        {
            var result = await _lesmateriaalInhoudRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] LesmateriaalInhoud lesmateriaalInhoud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesmateriaalInhoudRepository.Create(lesmateriaalInhoud);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LesmateriaalInhoud lesmateriaalInhoud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesmateriaalInhoudRepository.Update(id, lesmateriaalInhoud);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lesmateriaalInhoudRepository.Delete(id);
            return Ok();
        }
    }
}
