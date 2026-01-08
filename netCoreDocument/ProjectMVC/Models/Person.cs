using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{
     [Table("Person")]
    public class Person
    {
       
        [Key]
        [Required]
        public string PersonId {get; set;} = default!;
        public string FullName {get; set; } =default!;

    }
}