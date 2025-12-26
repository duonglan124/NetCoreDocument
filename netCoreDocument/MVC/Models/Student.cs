using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace MVC.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Required]
        public string StudentId {get; set;} = default!;
        public string StudentName {get; set; } = default!;

    }

}


