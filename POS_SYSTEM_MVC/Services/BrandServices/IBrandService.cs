using POS_SYSTEM_MVC.DTOs;

namespace POS_SYSTEM_MVC.Services.Brands;

public interface IBrandService
{
    Task<int> AddBrandAsync(AddBrandDto dto);
}