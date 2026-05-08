namespace POS_SYSTEM_MVC.DTOs.Dashboard
{
    public class TopProductDto
    {
        public int Rank { get; set; }
        public string ProductName { get; set; }
        public int UnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
