namespace MusiciansBlog.API.Authentication.Hashers
{
    /// <summary>
    /// Хэшер.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Метод по хэшированию пароля пользователя для БД.
        /// </summary>
        /// <param name="password">исходный пароль</param>
        /// <returns></returns>
        string Generate(string password);

        bool VerifyPassword(string password, string hashedPassword);
    }
}
