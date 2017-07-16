using System;

namespace APIass2.Models
{
    /// <summary>
    /// DTO used for each course in the list of all the courses of a semester
    /// </summary>
    public class CourseLiteDTO
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
        /// Number of all the students in the course
        /// </summary>
        public int NumberOfStudents { get; set; }
    }
}