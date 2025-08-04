namespace MusiciansBlog.API.Authentication.Hashers
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <inheritdoc />
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
