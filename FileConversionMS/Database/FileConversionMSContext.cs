using FileConversionMS.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileConversionMS.Database
{
    public class FileConversionMSContext : DbContext
    {

        public FileConversionMSContext(DbContextOptions<FileConversionMSContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LetterTemplate>()
           .Property(e => e.LetterType)
           .HasConversion<string>();            
            
            modelBuilder.Entity<LetterTemplate>()
           .Property(e => e.FontType)
           .HasConversion<string>();

            modelBuilder.Entity<LetterFile>()
                .Property(l => l.StudentExternalGuid)
                .IsRequired();
        }
        public DbSet<LetterTemplate> LetterTemplates { get; set; } = default!;
        public DbSet<LetterFile> LetterFiles { get; set; } = default!;
    }
}