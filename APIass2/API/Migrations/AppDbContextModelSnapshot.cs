using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using APIass2.Services;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("APIass2.NewServices.Entities.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Semester");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TemplateID");

                    b.HasKey("ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("APIass2.NewServices.Entities.CourseTemplate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("CourseTemplates");
                });

            modelBuilder.Entity("APIass2.NewServices.Entities.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("SSN");

                    b.HasKey("ID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("APIass2.NewServices.Entities.StudentsInCourses", b =>
                {
                    b.Property<string>("ID");

                    b.Property<int>("CourseID");

                    b.Property<int>("StudentID");

                    b.HasKey("ID");

                    b.ToTable("StudentsInCourses");
                });
        }
    }
}
