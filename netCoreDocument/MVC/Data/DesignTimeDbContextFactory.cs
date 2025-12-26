using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MVC.Data
{
    public class ApplicationDbcontextFactory 
        : IDesignTimeDbContextFactory<ApplicationDbcontext>
    {
        public ApplicationDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbcontext>();

            optionsBuilder.UseSqlite(
                "Data Source=app.db"
            );

            return new ApplicationDbcontext(optionsBuilder.Options);
        }
    }
}
