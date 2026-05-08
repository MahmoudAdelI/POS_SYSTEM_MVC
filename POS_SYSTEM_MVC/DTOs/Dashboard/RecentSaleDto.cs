namespace POS_SYSTEM_MVC.DTOs.Dashboard
{
    public class RecentSaleDto
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // "Completed" or "Pending"
        public DateTime Date { get; set; }
    }
}
