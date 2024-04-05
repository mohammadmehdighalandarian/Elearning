using System.ComponentModel.DataAnnotations;

namespace LearningWeb_Core.DTOs.UserPanel
{
    public class ChangePasswordViewModel
    {
       
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید.")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required(ErrorMessage = "عدم تطابق رمز .")]
        [MaxLength(200)]
        public string RePassword { get; set; }
    }
}
