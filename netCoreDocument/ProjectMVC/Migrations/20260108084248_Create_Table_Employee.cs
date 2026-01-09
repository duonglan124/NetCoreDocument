using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_Employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) // phương thức Up() để tạo bảng
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new // khai báo cột của bảng
                {
                    PersonId = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => // khai báo ràng buộc
                {
                    table.PrimaryKey("PK_Employee", x => x.PersonId);// khai báo khóa chính
                    table.ForeignKey(
                        name: "FK_Employee_Person_PersonId",// khai báo khóa ngoại
                        column: x => x.PersonId,// cột khóa ngoại
                        principalTable: "Person",// 
                        principalColumn: "PersonId",//  cột khóa chính của bảng cha
                        onDelete: ReferentialAction.Cascade);// hành động khi xóa bản ghi cha
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
