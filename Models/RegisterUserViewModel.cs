using System.ComponentModel.DataAnnotations;

namespace DiplomMetod.Models
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "имя должно быть от 2 до 16 символов")]
        public string? FirstName { get; set; }


        [Display(Name = "Фамилия")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "имя должно быть от 2 до 30 символов")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль должен содержать минимум {2} символов", MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

        [Required(ErrorMessage = "не указан email")]
        [EmailAddress(ErrorMessage = "некорректный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

    }
}

