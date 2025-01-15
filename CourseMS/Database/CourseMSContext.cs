using CourseMS.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseMS.Database
{
    public class CourseMSContext : DbContext
    {
        public CourseMSContext(DbContextOptions<CourseMSContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<CourseStudent> Students { get; set; } = default!;
    }
}
