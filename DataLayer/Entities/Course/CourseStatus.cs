using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public long StatusId { get; set; }

        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }


        #region Relation

        public List<Course> Courses { get; set; }

        #endregion

    }
}
