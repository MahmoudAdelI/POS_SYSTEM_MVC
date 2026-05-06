using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Brands;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.Brands;

public class BrandService(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    : IBrandService
{
    public async Task<int> AddBrandAsync(AddBrandDto dto)
    {
        var brand = new Brand { Name = dto.Name };

        await brandRepository.AddAsync(brand);
        await unitOfWork.SaveChangesAsync();

        return brand.Id;
    }
}