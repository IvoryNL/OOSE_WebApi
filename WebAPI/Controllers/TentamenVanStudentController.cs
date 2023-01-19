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
    public class TentamenVanStudentController : ControllerBase
    {
        private readonly IRepository<TentamenVanStudent> _tentamenVanStudentRepository;

        public TentamenVanStudentController(IRepository<TentamenVanStudent> tentamenVanStudentRepository)
        {
            _tentamenVanStudentRepository = tentamenVanStudentRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<TentamenVanStudent>>> Get()
        {
            var result = await _tentamenVanStudentRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TentamenVanStudent>> Get(int id)
        {
            var result = await _tentamenVanStudentRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] TentamenVanStudent tentamenVanStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tentamenVanStudentRepository.Create(tentamenVanStudent);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TentamenVanStudent tentamenVanStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tentamenVanStudentRepository.Update(id, tentamenVanStudent);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tentamenVanStudentRepository.Delete(id);
            return Ok();
        }
    }
}
