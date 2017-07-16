using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIass2.Models
{
    /// <summary>
    /// DTO for a student in a course
    /// </summary>
    public class StudentInCourseDTO
    {
        /// <summary>
        /// Name of the student, e.g. "Jón Jónsson"
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The student's social security number (SSN)
        /// </summary>
        public string SSN { get; set; }
    }
}