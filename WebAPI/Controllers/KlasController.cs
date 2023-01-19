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
    public class KlasController : ControllerBase
    {
        private readonly IRepository<Klas> _klasRepository;

        public KlasController(IRepository<Klas> klasRepository)
        {
            _klasRepository = klasRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Klas>>> Get()
        {
            var result = await _klasRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Klas>> Get(int id)
        {
            var result = await _klasRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Klas klas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _klasRepository.Create(klas);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Klas klas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _klasRepository.Update(id, klas);
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
            await _klasRepository.Delete(id);
            return Ok();
        }
    }
}
