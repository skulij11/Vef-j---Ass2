using System;
using System.Collections.Generic;

namespace APIass2.Models
{
    /// <summary>
    /// DTO for course details
    /// </summary>
    public class CourseDetailsDTO
    {   
        /// <summary>
        /// Name of the course, e.g. "Vefþjónustur"
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ID number of the course, e.g. "T-514-VEFT"
        /// </summary>
        public string CourseID { get; set; }
        /// <summary>
        /// The semester when the course is taught, e.g. 20152" -> summer 2015, "20153" -> fall 2015 etc.
        /// </summary>
        public string Semester { get; set; }
        /// <summary>
        /// End date for the course
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Start date for the course
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// List of all the students in this course
        /// </summary>
        public List<StudentInCourseDTO> Students { get; set; }
    }
}