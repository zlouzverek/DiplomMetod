using System.ComponentModel.DataAnnotations;

namespace DiplomMetod.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }

        public string UserId { get; set; }
        public string Email { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
