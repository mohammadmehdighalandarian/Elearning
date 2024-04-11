using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Course
{
    public class UserCourse
    {
        [Key]
        public long UC_Id { get; set; }
        public long UserId { get; set; }
        public long CourseId { get; set; }


        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }
    }
}
