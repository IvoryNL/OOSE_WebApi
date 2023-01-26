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
    public class TentamenController : ControllerBase
    {
        private readonly ITentamenRepository<Tentamen> _tentamenRepository;

        public TentamenController(ITentamenRepository<Tentamen> tentamenRepository)
        {
            _tentamenRepository = tentamenRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Tentamen>>> Get()
        {
            var result = await _tentamenRepository.GetAll();
            return Ok(result);
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Tentamen>> Get(int id)
        {
            var result = await _tentamenRepository.GetById(id);
            return Ok(result);
        }           

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Tentamen tentamen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tentamenRepository.Create(tentamen);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tentamen tentamen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tentamenRepository.Update(id, tentamen);
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
            await _tentamenRepository.Delete(id);
            return Ok();
        }

        [HttpGet("GetAllTentamensVanOnderwijsuitvoeringStudent/{id}")]
        public async Task<ActionResult<List<Tentamen>>> GetAllTentamensVanOnderwijsuitvoeringStudent(int id)
        {
            var result = await _tentamenRepository.GetAllTentamensVanOnderwijsuitvoeringStudent(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("InplannenTentamen/{id}")]
        public async Task<IActionResult> InplannenTentamen(int id, [FromBody] Tentamen tentamen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tentamenRepository.InplannenTentamen(id, tentamen);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("VerwijderPlanningVanTentamen/{id}/{planningId}")]
        public async Task<IActionResult> VerwijderPlanningVanTentamen(int id, int planningId)
        {
            await _tentamenRepository.VerwijderPlanningVanTentamen(id, planningId);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("KoppelLeeruitkomstAanTentamen/{id}")]
        public async Task<IActionResult> KoppelLeeruitkomstAanTentamen(int id, [FromBody] Tentamen tentamen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tentamenRepository.KoppelLeeruitkomstAanTentamen(id, tentamen);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("OntkoppelLeeruitkomstVanTentamen/{id}/{leeruitkomstId}")]
        public async Task<IActionResult> OntkoppelLeeruitkomstVanTentamen(int id, int leeruitkomstId)
        {
            await _tentamenRepository.OntkoppelLeeruitkomstVanTentamen(id, leeruitkomstId);
            return Ok();
        }
    }
}
