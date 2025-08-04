using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public sealed class UserModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid UserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Никнейм пользователя.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Токен обновления.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Дата истечения токена обновления.
        /// </summary>
        public DateTime RefreshTokenExpiry { get; set; }

        /// <summary>
        /// Тип аутентификации.
        /// </summary>
        public AuthType AuthType { get; set; }
    }
}
