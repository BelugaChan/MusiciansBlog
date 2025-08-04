namespace MusiciansBlog.API.Authentication.Providers
{
    /// <summary>
    /// Провайдер по добавлению токенов в cookie.
    /// </summary>
    public interface ICookieProvider
    {
        /// <summary>
        /// Метод по добавлению токенов в cookie.
        /// </summary>
        /// <param name="key">наименование токена (access, refresh)</param>
        /// <param name="tokenValue">значение токена</param>
        /// <param name="tokenExpiry">дата истечения токена</param>
        void AppendTokenToCookie(string key, string tokenValue, DateTime tokenExpiry);
    }
}
