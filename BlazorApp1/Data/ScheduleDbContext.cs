using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<LessonType> LessonTypes => Set<LessonType>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonStudent> LessonStudents => Set<LessonStudent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasKey(d => d.Code);
        modelBuilder.Entity<Department>().Property(d => d.Code).IsRequired();
        modelBuilder.Entity<Department>().Property(d => d.Name).IsRequired();

        modelBuilder.Entity<Classroom>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Classroom>().Property(c => c.Name).IsRequired();
        modelBuilder.Entity<Classroom>().Property(c => c.Code).IsRequired();

        modelBuilder.Entity<Subject>().Property(s => s.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Subject>().Property(s => s.Name).IsRequired();
        modelBuilder.Entity<Subject>().Property(s => s.SubjectCode).IsRequired();
        modelBuilder.Entity<Subject>().Property(s => s.DepartmentCode).IsRequired();
        modelBuilder.Entity<Subject>().HasIndex(s => s.SubjectCode).IsUnique();

        modelBuilder.Entity<LessonType>().Property(l => l.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<LessonType>().Property(l => l.Name).IsRequired();

        modelBuilder.Entity<Role>().Property(r => r.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Role>().Property(r => r.RoleType).IsRequired();

        modelBuilder.Entity<Person>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired();
        modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired();
        modelBuilder.Entity<Person>().Property(p => p.Affiliation).IsRequired();

        modelBuilder.Entity<Lesson>().Property(l => l.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<LessonStudent>()
            .HasKey(ls => new { ls.LessonId, ls.PersonId });

        modelBuilder.Entity<LessonStudent>()
            .HasOne<Lesson>()
            .WithMany()
            .HasForeignKey(ls => ls.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LessonStudent>()
            .HasOne<Person>()
            .WithMany()
            .HasForeignKey(ls => ls.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
