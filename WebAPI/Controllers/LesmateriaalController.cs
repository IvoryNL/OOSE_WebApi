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
    public class LesmateriaalController : ControllerBase
    {
        private readonly IRepository<Lesmateriaal> _lesmateriaalRepository;

        public LesmateriaalController(IRepository<Lesmateriaal> lesmateriaalRepository)
        {
            _lesmateriaalRepository = lesmateriaalRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Lesmateriaal>>> Get()
        {
            var result = await _lesmateriaalRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Lesmateriaal>> Get(int id)
        {
            var result = await _lesmateriaalRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Lesmateriaal lesmateriaal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _lesmateriaalRepository.Create(lesmateriaal);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Lesmateriaal lesmateriaal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _lesmateriaalRepository.Update(id, lesmateriaal);
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
