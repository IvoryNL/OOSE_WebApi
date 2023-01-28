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
    public class BeoordelingsmodelController : ControllerBase
    {
        private readonly IBeoordelingsmodelRepository<Beoordelingsmodel> _beoordelingsmodelRepository;

        public BeoordelingsmodelController(IBeoordelingsmodelRepository<Beoordelingsmodel> beoordelingsmodelRepository)
        {
            _beoordelingsmodelRepository = beoordelingsmodelRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Beoordelingsmodel>>> Get()
        {
            var result = await _beoordelingsmodelRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Beoordelingsmodel>> Get(int id)
        {
            var result = await _beoordelingsmodelRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Beoordelingsmodel beoordelingsmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsmodelRepository.Create(beoordelingsmodel);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Beoordelingsmodel beoordelingsmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingsmodelRepository.Update(id, beoordelingsmodel);
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
            await _beoordelingsmodelRepository.Delete(id);
            return Ok();
        }

        [HttpGet("GetBeoordelingsmodelByTentamenId/{id}")]
        public async Task<ActionResult<Beoordelingsmodel>> GetBeoordelingsmodelByTentamenId(int id)
        {
            var result = await _beoordelingsmodelRepository.GetBeoordelingsmodelByTentamenId(id);
            return Ok(result);
        }
    }
}
