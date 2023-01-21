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
    public class RolController : ControllerBase
    {
        private readonly IRepository<Rol> _rolRepository;

        public RolController(IRepository<Rol> rolRepository)
        {
            _rolRepository = rolRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Rol>>> Get()
        {
            var result = await _rolRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Rol>> Get(int id)
        {
            var result = await _rolRepository.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _rolRepository.Create(rol);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _rolRepository.Update(id, rol);
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
            await _rolRepository.Delete(id);
            return Ok();
        }
    }
}
