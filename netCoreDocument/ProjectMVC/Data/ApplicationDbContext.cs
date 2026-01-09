using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;

namespace MVC.Data //    namespace của dự án
{
    public class ApplicationDbContext : DbContext // lớp kế thừa từ DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)// constructor
            : base(options)// gọi constructor của lớp cha
        {
        }

        public DbSet<ProjectMVC.Models.Person> Person { get; set; } = default!; //  DbSet cho bảng Person
        public DbSet<ProjectMVC.Models.Employee> Employee { get; set; } = default!;
        public DbSet<ProjectMVC.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;
        public DbSet<ProjectMVC.Models.DaiLy> DaiLy { get; set; } = default!;
    }
}
