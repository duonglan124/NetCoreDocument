using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{
   [Table("Employee")]
   public class Employee : Person 
    {
      
        [Required]
        public string EmployeeId {get; set; } = default!;
        public int Age {get; set; }
    } 
}