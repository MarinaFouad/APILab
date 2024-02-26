using Lab1.Model;
using Microsoft.EntityFrameworkCore;


namespace Lab1.Entity
{
    public class UnivDbContext : DbContext
    {
        UnivDbContext() { }
        public UnivDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Manager> Managers { get; set; }

    }
}
