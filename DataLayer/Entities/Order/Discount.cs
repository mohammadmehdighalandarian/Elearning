using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Order
{
    public class Discount
    {
        [Key]
        public long DiscountId { get; set; }


        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long DiscountPercent { get; set; }

        public long? UsableCount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }
}
