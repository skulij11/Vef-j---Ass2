using System;
using System.Linq;
using System.Collections.Generic;
using APIass2.Models;
using APIass2.Services.Entities;

namespace APIass2.Services
{
    /// <summary>
    /// The API service
    /// </summary>
    public class CoursesService : ICoursesService
    {
        /// <summary>
        /// The database connection (in Project.db)
        /// </summary>
        private readonly AppDbContext _db;

        public CoursesService(AppDbContext db) 
        {
            _db = db;
        }

        /// <summary>
        /// Returns the all the courses for given semester (CourseLiteDTO list). 
        /// If no semester is given (semester == null) then the current semester is used (20163).
        /// </summary>
        public List<CourseLiteDTO> GetCoursesBySemester(string semester)
        {
            if(semester == null) 
            {
                semester = "20163";
            }
            
            return (from x in _db.Courses
            join ct in _db.CourseTemplates on x.TemplateID equals ct.ID  
            where x.Semester == semester
            select new CourseLiteDTO 
            {
                CourseID = ct.CourseID,
                Name = ct.Name,
                Semester = x.Semester,
                // The number of all the students in the course
                NumberOfStudents = _db.StudentsInCourses.Where(n => n.CourseID == x.ID).Count(),
                EndDate = x.EndDate,
                StartDate = x.StartDate
            }).ToList();
        }

        /// <summary>
        /// Used to return a list of all the student (CourseDetailsDTO.Students) in a given course
        /// </summary>
        public List<StudentInCourseDTO> GetStudentsInCourse(int courseID) {

            var course = GetCourseByID(courseID);
            // The course exists
            if(course != null) {
                return course.Students;
            }
            // If no course can be found, return null
            return null;
        }

        /// <summary>
        /// Used to return details about a given course with a list of all of the students in that course (CourseDetailsDTO)
        /// </summary>
        public CourseDetailsDTO GetCourseByID(int courseID)
        {
            var course = (from x in _db.Courses
            join ct in _db.CourseTemplates on x.TemplateID equals ct.ID  
            where x.ID == courseID
            select new CourseDetailsDTO 
            {
                CourseID = ct.CourseID,
                Name = ct.Name,
                Semester = x.Semester,
                EndDate = x.EndDate,
                StartDate = x.StartDate
            }).SingleOrDefault();

            // The course exists
            if(course != null) {
                // Find the students 
                var students = (from sic in _db.StudentsInCourses 
                join s in _db.Students on sic.StudentID equals s.ID   
                where sic.CourseID == courseID
                select new StudentInCourseDTO 
                {
                    Name = s.Name,
                    SSN = s.SSN
                }).ToList();

                // Put the list of the students into the DTO
                course.Students = students;
            }
            // Returns null if it doesn't exist
            return course;
        }

        /// <summary>
        /// Used to update a chosen course's end dates and start dates from the CourseViewModel
        /// </summary>
        public void UpdateCourse(CourseViewModel changedCourse) 
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == changedCourse.CourseID);
            // Should return a single instance or null

            // It doesn't exist we throw an exception
            if (course == null) 
            {
                throw new ArgumentException();
            }
            course.EndDate = changedCourse.EndDate;
            course.StartDate = changedCourse.StartDate;

            _db.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing course from the database
        /// </summary>
        public void DeleteCourse(int courseID) 
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == courseID);

            // If the course doesn't exist we throw an exception
            if(course == null) 
            {
                throw new ArgumentException();
            }
            _db.Courses.Remove(course);

            // Remove all the students from the course in the connection table (StudentsInCourses)
            var studentsInTheCourse = _db.StudentsInCourses.Where(x => x.CourseID == courseID);
            _db.StudentsInCourses.RemoveRange(studentsInTheCourse);
            _db.SaveChanges();
        }
        
        /// <summary>
        /// Adds an existing student to an existing course using StudentToCourseViewModel 
        /// </summary>
        public void AddStudentToCourse(StudentToCourseViewModel studentInNewCourse)
        {
            // Check whether the student and course already exist, they should
            var student = _db.Students.SingleOrDefault(x => x.SSN == studentInNewCourse.SSN);
            var course = _db.Courses.SingleOrDefault(x => x.ID == studentInNewCourse.CourseID);
            
            // Else throw an exception
            if(student == null || course == null)
            {
                throw new ArgumentException();
            }

            // Check whether the the student is already registered in the course
            var inTheDb = _db.StudentsInCourses.SingleOrDefault(x => x.StudentID == student.ID && x.CourseID == course.ID);

            if(inTheDb == null) {  // Add a new entity to the connection table
                _db.StudentsInCourses.Add( new StudentsInCourses { StudentID = student.ID, CourseID = course.ID });
                _db.SaveChanges();
            }
        }
    }
}