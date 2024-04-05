using System.ComponentModel.DataAnnotations;

namespace LearningWeb_Core.DTOs.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید.")]
        [MaxLength(100)]
        public string userName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده متعبر نمیباشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید.")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required(ErrorMessage = "عدم تطابق رمز .")]
        [MaxLength(200)]
        public string RePassword { get; set; }

    }
}
