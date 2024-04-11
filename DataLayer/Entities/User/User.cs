using DataLayer.Entities.Course;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.User
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string ActivateCode { get; set; }

        public bool IsActive { get; set; } = false;

        public string UserAvatar { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool Isdeleted { get; set; }=false;

        

       
        #region Relation

        
        public  List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Course.Course> Courses { get; set; }
        public virtual List<Order.Order> Orders { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
        public List<CourseComment> CourseComments { get; set; }

        public List<CourseVote> CourseVotes { get; set; }

        #endregion

    }
}
