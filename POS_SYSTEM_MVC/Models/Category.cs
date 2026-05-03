using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = [];
    }
}
