using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Services.SubCategoriesServices;

namespace POS_SYSTEM_MVC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoryController(ISubCategoryService subCategoryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddSubCategoryDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await subCategoryService.AddSubCategoryAsync(dto);
            return Ok(new { id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}