namespace MusiciansBlog.Application.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Никнейм пользователя.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Дата истечения токена обновления.
        /// </summary>
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
