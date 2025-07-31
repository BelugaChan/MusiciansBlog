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
        public Guid UserId { get; set; } = Guid.Empty;

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
        public string PasswordHash { get; set; } = string.Empty;

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
