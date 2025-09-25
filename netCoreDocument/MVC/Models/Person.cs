using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("Person")]
    public class Person
    {

        [Key]
        [Required]
        public string PersonId { get; set; } = default!;
        [Required]
        public string FullName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

    }
}