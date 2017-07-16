using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APIass2.Models;
using APIass2.Services;

namespace APIass2.API.Controllers
{
    /// <summary>
    /// The API controller. Uses the service class CourseService
    /// </summary>
    [Route("api/courses")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _service;

        public CoursesController(ICoursesService service) 
        {
            _service = service;
        }

        /// <summary>
        /// Returns the list of courses by a semester
        /// </summary>
        [HttpGet]
        public List<CourseLiteDTO> GetCoursesBySemester(string semester = null)
        {
            //Tékka á ef semester gengur ekki upp!
            return _service.GetCoursesBySemester(semester);
        }

        /// <summary>
        /// Returns a course by its ID if it exists
        /// </summary>
        [HttpGet]
        [Route("{courseID}", Name = "GetCourse")]
        public IActionResult GetCourseByID(int courseID)
        {
            var course = _service.GetCourseByID(courseID);
            // If the course doesn't exist, we return 404 (Not found)
            if(course == null) 
            {
                return NotFound();
            }
            return new ObjectResult(course);
        }
        
        /// <summary>
        /// Updates an existing course, using CourseViewModel
        /// </summary>
        [HttpPut]
        [Route("{courseID}")]
        public IActionResult UpdateCourse(int courseID, [FromBody] CourseViewModel changedCourse) 
        {
            if(changedCourse == null)      // No view model, - 400 (Bad request)
            {
                return BadRequest();
            }
            changedCourse.CourseID = courseID;
            try 
            {
                _service.UpdateCourse(changedCourse);
            }
            catch(ArgumentException ex)     // The update failed, no course was found - 404
            {
                return NotFound();  
            }
            return Ok();            // It worked! - 200
        }

        /// <summary>
        /// Deletes an existing course  
        /// </summary>
        [HttpDelete]
        [Route("{courseID}")]
        public IActionResult DeleteCourse(int courseID) 
        {
            try
            {
                _service.DeleteCourse(courseID);
            }
            catch(ArgumentException ex)         // Failed, no course was found
            {
                return NotFound();
            }                        
            return NoContent();         // Worked, returns 204 (No content)
        }

        /// <summary>
        /// Returnes all students registered in an existing course
        /// </summary>
        [HttpGet]
        [Route("{courseID}/students", Name = "GetStudents")]
        public IActionResult GetStudentsInCourse(int courseID)
        {
            var studentList = _service.GetStudentsInCourse(courseID);

            if(studentList == null)     // The course doesn't exist 
            {       
                return NotFound();
            }
            return new ObjectResult(studentList);
        }

        /// <summary>
        /// Adds an existing student to an existing course, using StudentToCourseViewModel
        /// </summary>
        [HttpPost]
        [Route("{courseID}/students")]
        public IActionResult AddStudentToCourse(int courseID, [FromBody] StudentToCourseViewModel student) 
        {
            if(student == null)         // View model failed - 400
            {
                return BadRequest();
            }
            student.CourseID = courseID;

            try
            {
                _service.AddStudentToCourse(student);
            }
            catch(ArgumentException ex)         // No course could be found - 404
            {
                return NotFound();
            }
            // Student successfully registered to the course
             return CreatedAtRoute("GetStudents", new { studentSSN = student.SSN }, student);
            
        }
    }
}