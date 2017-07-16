
namespace APIass2.Services.Entities
{
    /// <summary>
    /// Entity model for the course template for the database
    /// </summary>
    public class CourseTemplate
    {
        /// <summary>
        /// ID for the database (dbID)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The ID for the course, e.g. "T-514-VEFT"
        /// </summary>
        public string CourseID { get; set; }
        /// <summary>
        /// The name of the course, e.g. "Vefþjónustur"
        /// </summary>
        public string Name { get; set; }

    }
}