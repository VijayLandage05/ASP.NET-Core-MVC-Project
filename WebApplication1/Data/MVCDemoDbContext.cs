using Microsoft.EntityFrameworkCore;
using EmployeeCRUD.Models.Domain;

namespace EmployeeCRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
