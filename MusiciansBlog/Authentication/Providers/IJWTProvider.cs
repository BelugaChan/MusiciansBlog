using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Authentication.Providers
{
    /// <summary>
    /// Провайдер по генерации токенов.
    /// </summary>
    public interface IJWTProvider
    {
        /// <summary>
        /// Генерация токена доступа.
        /// </summary>
        /// <param name="model">Экземпляр сущности.</param>
        /// <returns>сгенерированный токен</returns>
        string GenerateToken(UserModel model);

        /// <summary>
        /// Генерация refresh токена.
        /// </summary>
        /// <returns>refresh токен</returns>
        string GenerateRefreshToken();
    }
}
