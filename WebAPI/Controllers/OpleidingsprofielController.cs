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
    public class OpleidingsprofielController : ControllerBase
    {
        private readonly IOpleidingsprofielRepository<Opleidingsprofiel> _opleidingsprofielRepository;

        public OpleidingsprofielController(IOpleidingsprofielRepository<Opleidingsprofiel> opleidingsprofielRepository)
        {
            _opleidingsprofielRepository = opleidingsprofielRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Opleidingsprofiel>>> Get()
        {
            var result = await _opleidingsprofielRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Opleidingsprofiel>> Get(int id)
        {
            var result = await _opleidingsprofielRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Opleidingsprofiel opleidingsprofiel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _opleidingsprofielRepository.Create(opleidingsprofiel);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Opleidingsprofiel opleidingsprofiel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _opleidingsprofielRepository.Update(id, opleidingsprofiel);
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
            await _opleidingsprofielRepository.Delete(id);
            return Ok();
        }

        [HttpGet("GetAllOpleidingsprofielenByOpleidingId/{opleidingId}")]
        public async Task<ActionResult<List<Opleidingsprofiel>>> GetAllOpleidingsprofielenByOpleidingId(int opleidingId)
        {
            var result = await _opleidingsprofielRepository.GetAllOpleidingsprofielenByOpleidingId(opleidingId);
            return Ok(result);
        }
    }
}
