using Microsoft.EntityFrameworkCore;
using StaffScroll.Models.Entities;

namespace StaffScroll.DAL
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }
        //public virtual DbSet<Employee>Employees { get; set; }
    }
}
