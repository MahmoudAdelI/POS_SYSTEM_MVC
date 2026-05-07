using POS_SYSTEM_MVC.DTOs.SubCategory;

namespace POS_SYSTEM_MVC.DTOs.Category
{

    // used for GetAll with subcategories

    public class CategoryWithSubsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryResponseDto> SubCategories { get; set; }
    }
}
