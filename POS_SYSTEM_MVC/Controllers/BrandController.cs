// Controllers/BrandController.cs
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs.Brand;
using POS_SYSTEM_MVC.Services.Brands;
using POS_SYSTEM_MVC.Services.CategoryServices;

namespace POS_SYSTEM_MVC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController(IBrandService brandService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBrandDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await brandService.AddBrandAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await brandService.GetAllAsync();
        return Ok(categories);
    }
}