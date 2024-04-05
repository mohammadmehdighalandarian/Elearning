using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Permision
{
    public class Permision
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }
        public long? ParentID { get; set; }


        #region Relations

        [ForeignKey("ParentID")]
        public List<Permision> Parents { get; set;}

        public List<RolePermision> RolePermisions { get; set; }


        #endregion
    }
}
