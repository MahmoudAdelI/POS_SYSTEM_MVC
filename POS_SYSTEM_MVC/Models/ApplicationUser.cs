using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SYSTEM_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(25)]
        public string FirstName { get; set; }


        [MaxLength(25)]
        public string LastName { get; set; }


        public DateOnly? HireDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);


        [Column(TypeName = "smallmoney")]
        public decimal Salary { get; set; }

        public ICollection<Sale> Sales { get; set; } = [];
    }
}
