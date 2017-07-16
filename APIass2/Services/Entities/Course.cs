using System;

namespace APIass2.Services.Entities
{
    /// <summary>
    /// Entity model for the course table for the database
    /// </summary>
    public class Course
    {   
        /// <summary>
        /// ID for the database (dbID)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The ID from the CourseTemplate table
        /// </summary>
        public int TemplateID { get; set; }
        /// <summary>
        /// Semester of the course, e.g. 20151" -> spring 2015, "20152" -> summer 2015 etc.
        /// </summary>
        public string Semester { get; set; }
        /// <summary>
        /// Start date of the course
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// End date of the course
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
