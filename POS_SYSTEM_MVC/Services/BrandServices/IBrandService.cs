using POS_SYSTEM_MVC.DTOs.Brand;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.Services.Brands;

public interface IBrandService
{
    Task<BrandResponseDto> AddBrandAsync(AddBrandDto dto);
    Task<IReadOnlyList<BrandResponseDto>> GetAllAsync();
}