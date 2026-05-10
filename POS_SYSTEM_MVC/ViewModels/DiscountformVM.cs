using Microsoft.AspNetCore.Mvc.Rendering;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.ViewModels
{
    public class DiscountformVM
    {

        public DiscountVM Discount { get; set; } = new();

        public IReadOnlyList<Discount> Discounts { get; set; }
            = new List<Discount>();
        public IReadOnlyList<Product> Products { get; set; }
            = new List<Product>();
        public IReadOnlyList<ProductVariant> ProductVariants { get; set; }
            = new List<ProductVariant>();

    }
}
