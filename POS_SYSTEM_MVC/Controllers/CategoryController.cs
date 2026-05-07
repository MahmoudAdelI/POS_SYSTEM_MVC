
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs.Category;
using POS_SYSTEM_MVC.Services;
using POS_SYSTEM_MVC.Services.CategoryServices;

namespace POS_SYSTEM_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await categoryService.AddCategoryAsync(dto);
            return Ok(new { id });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
            {
                var categories = await categoryService.GetAllWithSubsAsync();
                return Ok(categories);
            } 
    }
    
}