using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs.SubCategory;
using POS_SYSTEM_MVC.Models;
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
            var subCategory = await subCategoryService.AddSubCategoryAsync(dto);
            return Ok(subCategory);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("attributes/{id}")]
    public async Task<IActionResult> GetAllAttributes(int id)
    {
        var res = await subCategoryService.GetAttributesWithValuesAsync(id);
        if(res == null)
            return NotFound($"Attributes with CategoryID {id} was not found!");
        return Ok(res);
    }
}