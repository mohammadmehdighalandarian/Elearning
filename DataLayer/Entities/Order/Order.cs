using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Order
{
    public class Order
    {

        [Key]
        public long OrderId { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }


        public virtual User.User User { get; set; }
       // public virtual List<OrderDetail> OrderDetails { get; set; }


    }
}
