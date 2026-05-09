namespace POS_SYSTEM_MVC.DTOs.Inventory
{
    public class InventoryProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int TotalStock { get; set; }
        public int VariantsCount { get; set; }
        public int OutOfStockVariants { get; set; }
        public int LowStockVariants { get; set; }
        public string StatusLabel => OutOfStockVariants > 0
          ? $"{OutOfStockVariants} Out of Stock"
          : LowStockVariants > 0
              ? $"{LowStockVariants} Low Stock"
              : "In Stock";

        public string StatusBadgeStyle => OutOfStockVariants > 0
            ? "background:#FDECEA;color:#C0392B"
            : LowStockVariants > 0
                ? "background:#FEF9E7;color:#B7770D"
                : "background:#EAF3DE;color:#3B6D11";
    }

    public class InventoryDto
    {
        public int TotalProducts { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public List<InventoryProductDto> Products { get; set; } = [];
    }
}
