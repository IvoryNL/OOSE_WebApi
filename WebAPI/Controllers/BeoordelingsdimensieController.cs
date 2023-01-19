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
    [Authorize(Roles = Rollen.DOCENT)]
    public class BeoordelingsdimensieController : ControllerBase
    {
        private readonly IRepository<Beoordelingsdimensie> _beoordelingsDimensieRepository;

        public BeoordelingsdimensieController(IRepository<Beoordelingsdimensie> beoordelingsDimensieRepository)
        {
            _beoordelingsDimensieRepository = beoordelingsDimensieRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Beoordelingsdimensie>>> Get()
        {
            var result = await _beoordelingsDimensieRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Beoordelingsdimensie>> Get(int id)
        {
            var result = await _beoordelingsDimensieRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Beoordelingsdimensie beoordelingsdimensie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsDimensieRepository.Create(beoordelingsdimensie);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Beoordelingsdimensie beoordelingsdimensie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsDimensieRepository.Update(id, beoordelingsdimensie);
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
            await _beoordelingsDimensieRepository.Delete(id);
            return Ok();
        }
    }
}
