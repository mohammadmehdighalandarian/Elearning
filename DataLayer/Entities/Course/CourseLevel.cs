using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public long LevelId { get; set; }

        [MaxLength(150)]
        [Required]
        public string LevelTitle { get; set; }

        #region Relation

        public List<Course> Courses { get; set; }

        #endregion
    }
}
