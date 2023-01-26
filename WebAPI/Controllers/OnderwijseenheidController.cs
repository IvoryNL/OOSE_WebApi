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
    public class OnderwijseenheidController : ControllerBase
    {
        private readonly IRepository<Onderwijseenheid> _onderwijseenheidRepository;

        public OnderwijseenheidController(IRepository<Onderwijseenheid> onderwijseenheidRepository)
        {
            _onderwijseenheidRepository = onderwijseenheidRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Onderwijseenheid>>> Get()
        {
            var result = await _onderwijseenheidRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Onderwijseenheid>> Get(int id)
        {
            var result = await _onderwijseenheidRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Onderwijseenheid onderwijseenheid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijseenheidRepository.Create(onderwijseenheid);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Onderwijseenheid onderwijseenheid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijseenheidRepository.Update(id, onderwijseenheid);
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
            await _onderwijseenheidRepository.Delete(id);
            return Ok();
        }
    }
}
