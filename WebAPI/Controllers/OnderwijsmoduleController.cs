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
    public class OnderwijsmoduleController : ControllerBase
    {
        private readonly IOnderwijsmoduleRepository<Onderwijsmodule> _onderwijsmoduleRepository;

        public OnderwijsmoduleController(IOnderwijsmoduleRepository<Onderwijsmodule> onderwijsmoduleRepository)
        {
            _onderwijsmoduleRepository = onderwijsmoduleRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Onderwijsmodule>>> Get()
        {
            var result = await _onderwijsmoduleRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Onderwijsmodule>> Get(int id)
        {
            var result = await _onderwijsmoduleRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Onderwijsmodule onderwijsmodule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijsmoduleRepository.Create(onderwijsmodule);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Onderwijsmodule onderwijsmodule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijsmoduleRepository.Update(id, onderwijsmodule);
            }
            catch (HttpRequestException ex)
            {
                var message = "Er bestaat al een onderwijsmodule met deze naam";
                return new ConflictObjectResult(message);
            }

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _onderwijsmoduleRepository.Delete(id);
            return Ok();
        }

        [HttpGet("GetAllOnderwijsmodulesViaOpleidingId/{opleidingId}")]
        public async Task<ActionResult<List<Onderwijsmodule>>> GetAllOnderwijsmodulesViaOpleidingId(int opleidingId)
        {
            var result = await _onderwijsmoduleRepository.GetAllOnderwijsmodulesViaOpleidingId(opleidingId);
            return Ok(result);
        }

        [HttpGet("GetOnderwijsmoduleVoorExportById/{id}")]
        public async Task<ActionResult<Onderwijsmodule>> GetOnderwijsmoduleVoorExportById(int id)
        {
            var result = await _onderwijsmoduleRepository.GetOnderwijsmoduleVoorExportById(id);
            return Ok(result);
        }

        [HttpPut("VoegOnderwijseenheidToe/{id}")]
        public async Task<IActionResult> VoegOnderwijseenheidToe(int id, [FromBody] Onderwijseenheid onderwijseenheid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _onderwijsmoduleRepository.VoegOnderwijseenheidToe(id, onderwijseenheid);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }
    }
}
