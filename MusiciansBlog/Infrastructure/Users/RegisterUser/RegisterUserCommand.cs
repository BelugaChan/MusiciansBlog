using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MusiciansBlog.API.Infrastructure.Users.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        /// <summary>
        /// Электронная почта.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [Length(3, 30, ErrorMessage = "Must be between 5 and 30 characters")]
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 20 символов")]
        [RegularExpression(@"^(?=[^А-Яа-я]*$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Пароль должен содержать цифры, спецсимволы, латинские буквы в верхнем и нижнем регистре и не должен содержать кириллицу")]
        public required string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователя.
        /// </summary>
        [Required(ErrorMessage = "Confirmation is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 8 до 20 символов")]
        [RegularExpression(@"^(?=[^А-Яа-я]*$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Пароль должен содержать цифры, спецсимволы, латинские буквы в верхнем и нижнем регистре и не должен содержать кириллицу")]
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }

        /// <summary>
        /// Никнейм пользователя.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [Length(3, 30, ErrorMessage = "Must be between 5 and 30 characters")]
        public required string Username { get; set; }
    }
}
