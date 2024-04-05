using System.ComponentModel.DataAnnotations;

namespace LearningWeb_Core.DTOs.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده متعبر نمیباشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید.")]
        [MaxLength(200)]
        public string Password { get; set; }

        public bool RemmeberMe { get; set; } = false;
    }
}
