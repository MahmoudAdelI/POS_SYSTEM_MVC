namespace POS_SYSTEM_MVC.Models
{
    public class SubCategoryAttribute
    {
        public int SubCategoryId { get; set; }
        public int AttributeId { get; set; }

        public SubCategory SubCategory { get; set; }
        public ProductAttribute Attribute { get; set; }
    }
}
