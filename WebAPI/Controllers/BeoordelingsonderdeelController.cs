using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;
using WebAPI.Entities;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeoordelingsonderdeelController : ControllerBase
    {
        private readonly IRepository<Beoordelingsonderdeel> _beoordelingsonderdeelRepository;

        public BeoordelingsonderdeelController(IRepository<Beoordelingsonderdeel> beoordelingsonderdeelRepository)
        {
            _beoordelingsonderdeelRepository = beoordelingsonderdeelRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Beoordelingsonderdeel>>> Get()
        {
            var result = await _beoordelingsonderdeelRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Beoordelingsonderdeel>> Get(int id)
        {
            var result = await _beoordelingsonderdeelRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Beoordelingsonderdeel beoordelingsonderdeel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsonderdeelRepository.Create(beoordelingsonderdeel);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Beoordelingsonderdeel beoordelingsonderdeel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsonderdeelRepository.Update(id, beoordelingsonderdeel);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _beoordelingsonderdeelRepository.Delete(id);
            return Ok();
        }
    }
}
