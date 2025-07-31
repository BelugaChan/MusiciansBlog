using System.Security.Cryptography;

namespace MusiciansBlog.API.Infrastructure.Users.Providers
{
    public class JWTProvider : IJWTProvider
    {
        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }
    }
}
