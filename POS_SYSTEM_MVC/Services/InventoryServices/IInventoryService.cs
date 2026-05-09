using POS_SYSTEM_MVC.DTOs.Inventory;
namespace POS_SYSTEM_MVC.Services.InventoryServices
{
    public interface IInventoryService
    {
        Task<InventoryDto> GetInventoryDataAsync(string? search = null);
    }
}
