﻿using Microsoft.AspNetCore.Authorization;
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
    public class OnderwijsmoduleController : ControllerBase
    {
        private readonly IRepository<Onderwijsmodule> _onderwijsmoduleRepository;

        public OnderwijsmoduleController(IRepository<Onderwijsmodule> onderwijsmoduleRepository)
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

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
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

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
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
                return new ConflictObjectResult(ex.Message);
            }
            
            return Ok();
        }

        [Authorize(Roles = $"{Rollen.DOCENT}, {Rollen.ADMIN}")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _onderwijsmoduleRepository.Delete(id);
            return Ok();
        }
    }
}
