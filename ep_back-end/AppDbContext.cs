using ep_back_end.Entities;
using Microsoft.EntityFrameworkCore;

namespace ep_back_end
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // DbSet for Employee model
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
