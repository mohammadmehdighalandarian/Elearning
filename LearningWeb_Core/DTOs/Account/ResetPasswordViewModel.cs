using System.ComponentModel.DataAnnotations;

namespace LearningWeb_Core.DTOs.Account
{
    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید.")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required(ErrorMessage = "عدم تطابق رمز .")]
        [MaxLength(200)]
        public string RePassword { get; set; }
    }
}
