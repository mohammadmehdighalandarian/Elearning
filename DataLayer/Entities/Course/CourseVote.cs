using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Course
{
    public class CourseVote
    {
        [Key]
        public long VoteId { get; set; }
        [Required]
        public long CourseId { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;


        public User.User User { get; set; }
        public Course Course { get; set; }
    }
}
