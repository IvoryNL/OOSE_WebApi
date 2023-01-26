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
    public class LeerdoelController : ControllerBase
    {
        private readonly IRepository<Leerdoel> _leerdoelRepository;
        private readonly IOnderwijsmoduleRepository<Onderwijsmodule> _onderwijsmoduleRepository;

        public LeerdoelController(IRepository<Leerdoel> leerdoelRepository, IOnderwijsmoduleRepository<Onderwijsmodule> onderwijsmoduleRepository)
        {
            _leerdoelRepository = leerdoelRepository;
            _onderwijsmoduleRepository = onderwijsmoduleRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Leerdoel>>> Get()
        {
            var result = await _leerdoelRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Leerdoel>> Get(int id)
        {
            var result = await _leerdoelRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Leerdoel leerdoel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _leerdoelRepository.Create(leerdoel);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Leerdoel leerdoel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _leerdoelRepository.Update(id, leerdoel);
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
            await _leerdoelRepository.Delete(id);
            return Ok();
        }

        private async Task<bool> IncreaseVersion(Onderwijseenheid onderwijseenheid)
        {
            foreach (var onderwijsmodule in onderwijseenheid.Onderwijsmodules)
            {
                await _onderwijsmoduleRepository.IncreaseVersion(onderwijsmodule.Id);
            }

            return true;
        }
    }
}
