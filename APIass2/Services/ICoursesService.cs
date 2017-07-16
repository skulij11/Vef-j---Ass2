using System.Collections.Generic;
using APIass2.Models;

namespace APIass2.Services
{
    /// <summary>
    /// The interface for CoursesService, used for dependency injection
    /// </summary>
    public interface ICoursesService
    {
        List<CourseLiteDTO> GetCoursesBySemester(string semester);
        CourseDetailsDTO GetCourseByID(int courseID);
        List<StudentInCourseDTO> GetStudentsInCourse(int courseID);
        void UpdateCourse(CourseViewModel changedCourse);
        void DeleteCourse(int courseID); 
        void AddStudentToCourse(StudentToCourseViewModel student);
    }
}