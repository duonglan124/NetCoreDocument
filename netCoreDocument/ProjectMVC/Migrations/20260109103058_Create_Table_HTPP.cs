using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMVC.Migrations // namespace của dự án
{
    /// <inheritdoc />
    public partial class Create_Table_HTPP : Migration // lớp kế thừa từ Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // phương thức Up() để tạo bảng
        {
            migrationBuilder.CreateTable( // tạo bảng
                name: "HeThongPhanPhoi",// tên bảng
                columns: table => new // khai báo cột của bảng
                {
                    MaHTPP = table.Column<string>(type: "TEXT", nullable: false), // khóa chính, không null
                    TenHTPP = table.Column<string>(type: "TEXT", nullable: false) // không null
                },
                constraints: table => // khai báo ràng buộc
                {
                    table.PrimaryKey("PK_HeThongPhanPhoi", x => x.MaHTPP); // khai báo khóa chính
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) // phương thức Down() để xóa bảng
        {
            migrationBuilder.DropTable(
                name: "HeThongPhanPhoi");
        }
    }
}
