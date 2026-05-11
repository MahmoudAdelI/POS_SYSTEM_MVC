using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.DTOs.Attribute;
using POS_SYSTEM_MVC.Services.AttributeServices;

namespace POS_SYSTEM_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController(IAttributeService attributeService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddAttributeDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var attribute = await attributeService.AddAttributeAsync(dto.Name);
                return Ok(attribute);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
