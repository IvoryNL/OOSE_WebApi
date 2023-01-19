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
    public class OnderwijsuitvoeringController : ControllerBase
    {
        private readonly IRepository<Onderwijsuitvoering> _onderwijsuitvoeringRepository;

        public OnderwijsuitvoeringController(IRepository<Onderwijsuitvoering> onderwijsuitvoeringRepository)
        {
            _onderwijsuitvoeringRepository = onderwijsuitvoeringRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Onderwijsuitvoering>>> Get()
        {
            var result = await _onderwijsuitvoeringRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Onderwijsuitvoering>> Get(int id)
        {
            var result = await _onderwijsuitvoeringRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Onderwijsuitvoering onderwijsuitvoering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijsuitvoeringRepository.Create(onderwijsuitvoering);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Onderwijsuitvoering onderwijsuitvoering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijsuitvoeringRepository.Update(id, onderwijsuitvoering);
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
            await _onderwijsuitvoeringRepository.Delete(id);
            return Ok();
        }
    }
}
