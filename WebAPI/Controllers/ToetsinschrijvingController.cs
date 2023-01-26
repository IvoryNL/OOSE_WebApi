using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToetsinschrijvingController : ControllerBase
    {
        private readonly IRepository<Toetsinschrijving> _toetsinschrijvingRepository;

        public ToetsinschrijvingController(IRepository<Toetsinschrijving> toetsinschrijvingRepository)
        {
            _toetsinschrijvingRepository = toetsinschrijvingRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Toetsinschrijving>>> Get()
        {
            var result = await _toetsinschrijvingRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Toetsinschrijving>> Get(int id)
        {
            var result = await _toetsinschrijvingRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Toetsinschrijving toetsinschrijving)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _toetsinschrijvingRepository.Create(toetsinschrijving);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Toetsinschrijving toetsinschrijving)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _toetsinschrijvingRepository.Update(id, toetsinschrijving);
            }
            catch (HttpRequestException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _toetsinschrijvingRepository.Delete(id);
            return Ok();
        }
    }
}
