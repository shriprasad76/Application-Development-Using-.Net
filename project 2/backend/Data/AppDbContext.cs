using CollegeLabEvalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeLabEvalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Marks> Marks => Set<Marks>();
        public DbSet<Batch> Batches => Set<Batch>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
                entity.HasOne(e => e.Batch).WithMany(b => b.Students).HasForeignKey(e => e.BatchId);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            });

            modelBuilder.Entity<Marks>(entity =>
            {
                entity.HasOne(e => e.Student).WithMany(s => s.Marks).HasForeignKey(e => e.StudentId);
                entity.HasOne(e => e.Subject).WithMany(s => s.Marks).HasForeignKey(e => e.SubjectId);
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).IsRequired().HasMaxLength(80);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            });
        }
    }
}
