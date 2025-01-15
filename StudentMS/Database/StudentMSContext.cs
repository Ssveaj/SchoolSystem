using Microsoft.EntityFrameworkCore;
using StudentMS.Database.Entities;

namespace StudentMS.Database
{
    public class StudentMSContext : DbContext
    {
        public StudentMSContext(DbContextOptions<StudentMSContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<Student> Students { get; set; } = default!;
    }
}
