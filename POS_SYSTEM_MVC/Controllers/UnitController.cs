using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Services.UnitServices;

namespace POS_SYSTEM_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IUnitService unitService) : ControllerBase
    {
        private readonly IUnitService _unitService = unitService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var units = await _unitService.GetAllAsync();
            return Ok(units);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();
            return Ok(unit);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(AddUnitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var unit = new Unit
            {
                Name = dto.Name
            };

            await _unitService.AddAsync(unit);

            return CreatedAtAction(nameof(GetById), new { id = unit.Id }, unit);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AddUnitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            unit.Name = dto.Name;

            await _unitService.UpdateAsync(unit);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            await _unitService.DeleteAsync(id);
            return NoContent();
        }
    }
}