using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVC.Models
{
    [Table("HeThongPhanPhoi")]
    public class HeThongPhanPhoi
    {

        [Key]
        [Required]
        public string MaHTPP { get; set; } = default!;
        [Required]
        public string TenHTPP{ get; set; } = default!;

    }
}