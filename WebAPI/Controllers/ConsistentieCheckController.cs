using Microsoft.AspNetCore.Mvc;
using WebAPI.Handlers.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsistentieCheckController : ControllerBase
    {
        private readonly IHandlerAsync<bool> _handler;

        public ConsistentieCheckController(IHandlerAsync<bool> handler)
        {
            _handler = handler;
        }

        [HttpGet("ConsistentieCheckCoverage/{onderwijsmoduleId}")]
        public async Task<ActionResult<bool>> ConsistentieCheckCoverage(int onderwijsmoduleId)
        {
            var result  = await _handler.ConsistentieCheckCoverageHandlerAsync(onderwijsmoduleId);
            
            return Ok(result);
        }
        [HttpGet("ConsistentieCheckTentamenPlanning/{onderwijsuitvoeringId}")]
        public async Task<ActionResult<bool>> ConsistentieCheckTentamenPlanning(int onderwijsuitvoeringId)
        {
            var result = await _handler.ConsistentieCheckTentamenPlanningHandlerAsync(onderwijsuitvoeringId);

            return Ok(result);
        }
    }
}
