using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Handlers.Interfaces;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConsistentieCheckController : ControllerBase
    {
        private readonly IHandlerAsync<bool> _handler;

        public ConsistentieCheckController(IHandlerAsync<bool> handler)
        {
            _handler = handler;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<bool>> Get(int id)
        {
            var test  = await _handler.GetAsync(id);
            
            return Ok();
        }
    }
}
