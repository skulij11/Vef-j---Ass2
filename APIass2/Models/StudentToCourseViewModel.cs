using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace APIass2.Models
{
    /// <summary>
    /// View model used to add an existing student to a course
    /// </summary>
    public class StudentToCourseViewModel
    {
        /// <summary>
        /// The ID of the course (from db)
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// The student's SSN
        /// </summary>
        public string SSN { get; set; }
    }
}