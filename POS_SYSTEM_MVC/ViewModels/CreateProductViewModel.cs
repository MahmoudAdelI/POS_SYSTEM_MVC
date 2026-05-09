using POS_SYSTEM_MVC.DTOs.Brand;
using POS_SYSTEM_MVC.DTOs.Category;
using POS_SYSTEM_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.ViewModels
{
    public class CreateProductViewModel
    {
        public IReadOnlyList<CategoryWithSubsDto> Categories { get; set; } = [];
        public IReadOnlyList<BrandResponseDto> Brands { get; set; } = [];
        public IEnumerable<Unit> Units { get; set; } = [];
    }
}
