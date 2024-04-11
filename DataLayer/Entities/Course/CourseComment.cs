using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public long CommentId { get; set; }
        public long CourseId { get; set; }
        public long UserId { get; set; }

        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }


        public Course Course { get; set; }
        public User.User User { get; set; }
    }
}
