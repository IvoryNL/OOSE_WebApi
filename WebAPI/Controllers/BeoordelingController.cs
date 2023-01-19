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
    public class BeoordelingController : ControllerBase
    {
        private readonly IRepository<Beoordeling> _beoordelingRepository;

        public BeoordelingController(IRepository<Beoordeling> beoordelingRepository)
        {
            _beoordelingRepository = beoordelingRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Beoordeling>>> Get()
        {
            var result = await _beoordelingRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Beoordeling>> Get(int id)
        {
            var result = await _beoordelingRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Beoordeling beoordeling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _beoordelingRepository.Create(beoordeling);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Beoordeling beoordeling)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _beoordelingRepository.Update(id, beoordeling);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _beoordelingRepository.Delete(id);
            return Ok();
        }
    }
}
