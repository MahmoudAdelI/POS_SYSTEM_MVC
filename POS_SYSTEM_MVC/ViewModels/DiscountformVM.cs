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

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
        public POS_SYSTEM_MVC.Constants.Enums.DiscountTypeENUM? FilterType { get; set; }
        public bool? FilterIsActive { get; set; }
    }
}
