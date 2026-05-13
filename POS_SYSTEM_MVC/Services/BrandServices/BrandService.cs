using POS_SYSTEM_MVC.DTOs.Brand;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.Brands;

public class BrandService(IUnitOfWork unitOfWork) : IBrandService
{
    public async Task<int> AddBrandAsync(AddBrandDto dto)
    {
        var brand = new Brand { Name = dto.Name };
        await unitOfWork.Brands.AddAsync(brand);
        await unitOfWork.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<IReadOnlyList<BrandResponseDto>> GetAllAsync()
    {
        var brands = await unitOfWork.Brands.GetAllAsync();
        return brands.Select(b => new BrandResponseDto { Id = b.Id, Name = b.Name }).ToList();
    }
}