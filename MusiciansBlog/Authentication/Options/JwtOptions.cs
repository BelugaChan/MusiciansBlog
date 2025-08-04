namespace MusiciansBlog.API.Authentication.Options
{
    /// <summary>
    /// Класс, реализующий паттерн Options.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Ключ для валидации jwt токена.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Издатель.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Подписчики.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Время жизни jwt токена.
        /// </summary>
        public int AccessTokenExpiryMinutes { get; set; }

        /// <summary>
        /// Время жизни refresh токена.
        /// </summary>
        public int RefreshTokenExpiryDays { get; set; }
    }
}
