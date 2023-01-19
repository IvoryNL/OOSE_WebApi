using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;
using WebAPI.Entities;
using WebAPI.Mappers.Interfaces;
using WebAPI.Models.Dto;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Rollen.DOCENT)]
    public class BeoordelingscriteriaController : ControllerBase
    {
        private readonly IRepository<Beoordelingscriteria> _beoordelingscriteriaRepository;

        public BeoordelingscriteriaController(IRepository<Beoordelingscriteria> beoordelingscriteriaRepository)
        {
            _beoordelingscriteriaRepository = beoordelingscriteriaRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Beoordelingscriteria>>> Get()
        {
            var result = await _beoordelingscriteriaRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Beoordelingscriteria>> Get(int id)
        {
            var result = await _beoordelingscriteriaRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Beoordelingscriteria beoordelingscriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingscriteriaRepository.Create(beoordelingscriteria);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Beoordelingscriteria beoordelingscriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beoordelingscriteriaRepository.Update(id, beoordelingscriteria);
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
            await _beoordelingscriteriaRepository.Delete(id);
            return Ok();
        }
    }
}
