using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class DaiLy
    {
        [Key]
        [Required]
        public string MaDaiLy { get; set; } = default!;

        [Required]
        public string TenDaiLy { get; set; } = default!;

        public string DiaChi { get; set; } =default!;

        public string NguoiDaiDien { get; set; } = default!;

        public string DienThoai { get; set; } = default!;

        // ---- Khóa ngoại ----
        [Required]
        public string MaHTPP { get; set; }= default!;

        [ForeignKey("MaHTPP")]
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }
    }
}
