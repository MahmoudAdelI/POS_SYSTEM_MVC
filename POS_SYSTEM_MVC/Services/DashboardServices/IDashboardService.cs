using POS_SYSTEM_MVC.DTOs.Dashboard;

namespace POS_SYSTEM_MVC.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardDataAsync();
    }
}
