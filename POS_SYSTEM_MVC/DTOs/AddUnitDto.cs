using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs
{
    public class AddUnitDto
    {
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
