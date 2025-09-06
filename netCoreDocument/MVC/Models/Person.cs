using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("Person")]
    public class Person
    {
      
        [Key]
        public string FullName { get; set; } 
        public int NamSinh { get; set; } 

    }
}