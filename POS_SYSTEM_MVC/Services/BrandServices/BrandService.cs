using POS_SYSTEM_MVC.DTOs.Brand;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.Brands;

public class BrandService(IUnitOfWork unitOfWork) : IBrandService
{
    public async Task<BrandResponseDto> AddBrandAsync(AddBrandDto dto)
    {
        var brand = new Brand { Name = dto.Name };
        await unitOfWork.Brands.AddAsync(brand);
        await unitOfWork.SaveChangesAsync();
        return new BrandResponseDto { Id = brand.Id, Name = brand.Name};
    }

    public async Task<IReadOnlyList<BrandResponseDto>> GetAllAsync()
    {
        var brands = await unitOfWork.Brands.GetAllAsync();
        return brands.Select(b => new BrandResponseDto { Id = b.Id, Name = b.Name }).ToList();
    }
}