using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.Product
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid brand")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Base price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
        [DataType(DataType.Currency)]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid subcategory")]
        public int SubcategoryId { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid unit")]
        public int UnitId { get; set; }

        // Validate variants exist
        [MinLength(1, ErrorMessage = "At least one variant is required")]
        public List<ProductVariantDto> Variants { get; set; }
    }
    public class ProductVariantDto
    {
        [Required]
        [Range(0.01, 999999.99, ErrorMessage = "Variant price must be between 0.01 and 999,999.99")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Each variant must have at least one attribute")]
        public List<int> AttributeValues { get; set; }
    }
}
