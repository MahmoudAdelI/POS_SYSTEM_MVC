using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Services.DiscountServices
{
    public interface IDiscountService
    {
        //Task CreateAsync(DiscountVM model);
        Task<List<string>> CreateAsync(DiscountVM model);
        //void ValidateDiscount(DiscountVM model);
        List<string> ValidateDiscount(DiscountVM model);
        Discount MapToDiscount(DiscountVM model); 
        Task<IReadOnlyList<Discount>>GetAllAsync();
    }
}
