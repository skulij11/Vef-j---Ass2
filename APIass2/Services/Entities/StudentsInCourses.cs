
namespace APIass2.Services.Entities
{
    /// <summary>
    /// Entity model for the table to connect a student and a course in the database
    /// </summary>
    public class StudentsInCourses
    {   
        /// <summary>
        /// ID for the database (dbID)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID for the student (dbID)
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// ID for the course (dbID)
        /// </summary>
        public int CourseID { get; set; }
    }
}
