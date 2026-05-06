using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POS_SYSTEM_MVC.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
    }
}
