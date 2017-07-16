using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace APIass2.Models
{
    /// <summary>
    /// View model for a course, used to update dates (start/end)
    /// </summary>
    public class CourseViewModel
    {   
        /// <summary>
        /// ID number for the course (from db)
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// End date for the course
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Start date for the course
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}