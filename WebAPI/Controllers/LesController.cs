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
    public class LesController : ControllerBase
    {
        private readonly ILesRepository<Les> _lesRepository;

        public LesController(ILesRepository<Les> lesRepository)
        {
            _lesRepository = lesRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Les>>> Get()
        {
            var result = await _lesRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Les>> Get(int id)
        {
            var result = await _lesRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.Create(les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.Update(id, les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lesRepository.Delete(id);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("KoppelLesmateriaalAanLes/{id}")]
        public async Task<IActionResult> KoppelLesmateriaalAanLes(int id, [FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.KoppelLesmateriaalAanLes(id, les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("OntkoppelLesmateriaalVanLes/{id}/{lesmateriaalId}")]
        public async Task<IActionResult> OntkoppelLesmateriaalVanLes(int id, int lesmateriaalId)
        {
            await _lesRepository.OntkoppelLesmateriaalVanLes(id, lesmateriaalId);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("KoppelLeeruitkomstAanLes/{id}")]
        public async Task<IActionResult> KoppelLeeruitkomstAanLes(int id, [FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.KoppelLeeruitkomstAanLes(id, les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("OntkoppelLeeruitkomstVanLes/{id}/{lesmateriaalId}")]
        public async Task<IActionResult> OntkoppelLeeruitkomstVanLes(int id, int lesmateriaalId)
        {
            await _lesRepository.OntkoppelLeeruitkomstVanLes(id, lesmateriaalId);
            return Ok();
        }
        

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("InplannenLes/{id}")]
        public async Task<IActionResult> InplannenLes(int id, [FromBody] Les les)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lesRepository.InplannenLes(id, les);
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("VerwijderPlanningVanLes/{id}/{planningId}")]
        public async Task<IActionResult> VerwijderPlanningVanLes(int id, int planningId)
        {
            await _lesRepository.VerwijderPlanningVanLes(id, planningId);
            return Ok();
        }
    }
}
