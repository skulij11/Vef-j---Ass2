
namespace APIass2.Services.Entities
{
    /// <summary>
    /// Entity model for a student for the database
    /// </summary>
    public class Student
    {   
        /// <summary>
        /// ID for the database (dbID)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Social security number (SSN)
        /// </summary>
        public string SSN { get; set; }
        /// <summary>
        /// The name of the student, e.g. "J�n J�nsson"
        /// </summary>
        public string Name { get; set; }
    }
}
