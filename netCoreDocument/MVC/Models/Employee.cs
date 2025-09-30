using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("Employee")]
    public class Employee : Person
    {
        [Required]
        public string EmployeeId { get; set; } = default!;

        [Required]
        public int Age { get; set; }
    }
}
