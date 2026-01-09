using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{

[Table("DaiLy")]
public class DaiLy
{
    [Key]
    [StringLength(20)]
    public string MaDaiLy { get; set; }

    public string TenDaiLy { get; set; }
    public string DiaChi { get; set; }
    public string NguoiDaiDien { get; set; }
    public string DienThoai { get; set; }

    // Foreign Key
    [Required]
    [StringLength(20)]
    public string MaHTPP { get; set; }

    // Navigation
    public HeThongPhanPhoi HeThongPhanPhoi { get; set; }
}
}
