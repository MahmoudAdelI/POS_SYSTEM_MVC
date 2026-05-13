namespace POS_SYSTEM_MVC.DTOs.Dashboard
{
    public class DashboardDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }

        public double SalesGrowth { get; set; }
        public double OrdersGrowth { get; set; }

        public List<RecentSaleDto> RecentSales { get; set; } = new();
        public List<TopProductDto> TopProducts { get; set; } = new();
        public List<SalesChartDto> SalesChart { get; set; } = new();
    }
}
