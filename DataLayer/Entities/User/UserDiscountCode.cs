using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Order;

namespace DataLayer.Entities.User
{
    public class UserDiscountCode
    {
        [Key]
        public long UD_Id { get; set; }
        public long UserId { get; set; }
        public long DiscountId { get; set; }


        public User User { get; set; }
        public Discount Discount { get; set; }
    }
}
