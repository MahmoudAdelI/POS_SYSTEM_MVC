namespace POS_SYSTEM_MVC.DTOs.SalesHistory
{
    public class SalesHistoryDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrder { get; set; }
        public List<SaleDto> Sales { get; set; } = [];
    }

    public class SaleDto
    {
        public int Id { get; set; }
        public string OrderId => $"ORD-{Id:D3}";
        public string CashierName { get; set; } = string.Empty;
        public int ItemsCount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;

        public string StatusBadgeStyle => Status == "Completed"
            ? "background:#EAF3DE;color:#3B6D11"
            : "background:#FDECEA;color:#C0392B";
    }
}