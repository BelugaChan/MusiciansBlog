using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Infrastructure.Users.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MusiciansBlog.API.Authentication.Providers
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JwtOptions _options;
        public JWTProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        /// <inheritdoc />
        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }

        /// <inheritdoc />
        public string GenerateToken(UserModel model)
        {
            var tokenExpireDate = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                new Claim(ClaimTypes.Expiration, tokenExpireDate.ToString())
            };

            //Алгоритм кодирования
            var signingCredential = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: tokenExpireDate,
                signingCredentials: signingCredential,
                claims: claims
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
