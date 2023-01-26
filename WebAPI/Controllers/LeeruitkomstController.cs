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
    public class LeeruitkomstController : ControllerBase
    {
        private readonly ILeeruitkomstRepository<Leeruitkomst> _leeruitkomstRepository;
        private readonly IOnderwijsmoduleRepository<Onderwijsmodule> _onderwijsmoduleRepository;

        public LeeruitkomstController(ILeeruitkomstRepository<Leeruitkomst> leeruitkomstRepository, 
            IOnderwijsmoduleRepository<Onderwijsmodule> onderwijsmoduleRepository)
        {
            _leeruitkomstRepository = leeruitkomstRepository;
            _onderwijsmoduleRepository = onderwijsmoduleRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Leeruitkomst>>> Get()
        {
            var result = await _leeruitkomstRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetLeeruitkomstenByOpleidingId/{id}")]
        public async Task<ActionResult<List<Leeruitkomst>>> GetLeeruitkomstenByOpleidingId(int id)
        {
            var result = await _leeruitkomstRepository.GetLeeruitkomstenByOpleidingId(id);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Leeruitkomst>> Get(int id)
        {
            var result = await _leeruitkomstRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Leeruitkomst leeruitkomst)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _leeruitkomstRepository.Create(leeruitkomst);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Leeruitkomst leeruitkomst)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _leeruitkomstRepository.Update(id, leeruitkomst);
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
            await _leeruitkomstRepository.Delete(id);
            return Ok();
        }

        private async Task<bool> IncreaseVersion(Leerdoel leerdoel)
        {
            foreach (var onderwijsmodule in leerdoel.Onderwijseenheid.Onderwijsmodules)
            {
                await _onderwijsmoduleRepository.IncreaseVersion(onderwijsmodule.Id);
            }

            return true;
        }
    }
}
