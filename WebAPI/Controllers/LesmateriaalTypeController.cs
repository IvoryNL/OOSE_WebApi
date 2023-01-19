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
    public class LesmateriaalTypeController : ControllerBase
    {
        private readonly IRepository<LesmateriaalType> _lesmateriaalRepository;

        public LesmateriaalTypeController(IRepository<LesmateriaalType> lesmateriaalRepository)
        {
            _lesmateriaalRepository = lesmateriaalRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LesmateriaalType>>> Get()
        {
            var result = await _lesmateriaalRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LesmateriaalType>> Get(int id)
        {
            var result = await _lesmateriaalRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] LesmateriaalType lesmateriaalType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _lesmateriaalRepository.Create(lesmateriaalType);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LesmateriaalType lesmateriaalType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _lesmateriaalRepository.Update(id, lesmateriaalType);
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
            await _lesmateriaalRepository.Delete(id);
            return Ok();
        }
    }
}
