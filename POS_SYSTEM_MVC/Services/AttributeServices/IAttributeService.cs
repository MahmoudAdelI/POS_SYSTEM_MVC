using POS_SYSTEM_MVC.DTOs.Attribute;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.Services.AttributeServices
{
    public interface IAttributeService
    {
        Task<AttributeResponseDto> AddAttributeAsync(string name);
    }
}
