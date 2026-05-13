using POS_SYSTEM_MVC.DTOs.SalesHistory;

namespace POS_SYSTEM_MVC.Services.SalesHistoryServices
{
    public interface ISalesHistoryService
    {
        Task<SalesHistoryDto> GetSalesHistoryAsync(string? filter = "all");
    }
}