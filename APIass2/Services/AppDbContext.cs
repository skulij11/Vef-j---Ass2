using Microsoft.EntityFrameworkCore;
using APIass2.Services.Entities;


namespace APIass2.Services
{
    /// <summary>
    /// DbContext for the database (Project.db)
    /// </summary>
    public class AppDbContext : DbContext
    {
        // The tables in the database
        public DbSet<CourseTemplate> CourseTemplates { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentsInCourses> StudentsInCourses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
