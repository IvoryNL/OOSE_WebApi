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
    public class LesController : ControllerBase
    {
        private readonly IRepository<Les> _lesRepository;

        public LesController(IRepository<Les> lesRepository)
        {
            _lesRepository = lesRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Les>>> Get()
        {
            var result = await _lesRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Les>> Get(int id)
        {
            var result = await _lesRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.Create(les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.Update(id, les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lesRepository.Delete(id);
            return Ok();
        }
    }
}
