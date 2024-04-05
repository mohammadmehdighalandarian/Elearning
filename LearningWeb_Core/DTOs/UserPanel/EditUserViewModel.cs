using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LearningWeb_Core.DTOs.UserPanel
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید.")]
        [MaxLength(100)]
        public string userName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده متعبر نمیباشد")]
        public string Email { get; set; }

        
    }
}
