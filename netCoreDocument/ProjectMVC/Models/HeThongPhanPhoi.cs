using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{   
    [Table("HeThongPhanPhoi")]
    public class HeThongPhanPhoi
    {
        [Key]
        [Required] // không null
        public string MaHTPP {get; set;}
        public string TenHTPP {get; set;}

        public ICollection<DaiLy> DaiLys { get; set; }

    }
}