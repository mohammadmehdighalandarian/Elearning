using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long GroupId { get; set; }

        public long? SubGroup { get; set; }

        [Required]
        public long TeacherId { get; set; }

        [Required]
        public long StatusId { get; set; }

        [Required]
        public long LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CoursePrice { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [MaxLength(100)]
        public string DemoFileName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }


        #region Relation

        [ForeignKey("GroupId")] 
        public CourseGroup MainGroup { get; set; }

        [ForeignKey("SubGroup")]
        public CourseGroup Sub_Group { get; set; }

        [ForeignKey("TeacherId")] 
        public User.User User { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("LevelId")] 
        public CourseLevel CourseLevel { get; set; }

        
        public List<CourseEpisode> CourseEpisodes { get; set; }

        #endregion


       
    }
}
