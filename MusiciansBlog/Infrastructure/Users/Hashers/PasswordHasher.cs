namespace MusiciansBlog.API.Infrastructure.Users.Hashers
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
    }
}
