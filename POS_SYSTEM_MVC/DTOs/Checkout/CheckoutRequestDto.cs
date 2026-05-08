namespace POS_SYSTEM_MVC.DTOs.Checkout
{
    public class CheckoutRequestDto
    {
        public List<CartItemDto> Items { get; set; } = [];
    }

    public class CartItemDto
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
