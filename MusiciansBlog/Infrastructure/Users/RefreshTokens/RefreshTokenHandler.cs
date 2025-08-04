using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Exceptions;
using MusiciansBlog.API.Infrastructure.Users.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusiciansBlog.API.Infrastructure.Users.RefreshTokens
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJWTProvider _provider;
        private readonly JwtOptions _options;
        public RefreshTokenHandler(
            IUsersRepository usersRepository,
            IJWTProvider provider,
            IOptions<JwtOptions> options)
        {
            _usersRepository = usersRepository;
            _provider = provider;
            _options = options.Value;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = GetClaimsFromExpiredAccessToken(request.AccessToken);
            var userId = Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));

            var existingUser = await _usersRepository.GetByIdAsync(userId, cancellationToken);

            if(existingUser is null)
            {
                throw new EntityNotFoundException();
            }

            if(request.RefreshToken != existingUser.RefreshToken
                || existingUser.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new UnvalidRefreshTokenException();
            }

            var newRefreshToken = _provider.GenerateRefreshToken();
            var accessToken = _provider.GenerateToken(existingUser);
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiryDays);

            var isOk = await _usersRepository.UpdateAsync(userId,
                u =>
                {
                    u.RefreshToken = newRefreshToken;
                    u.RefreshTokenExpiry = refreshTokenExpiry;
                },
                cancellationToken
            );

            if (!isOk)
            {
                throw new DbUpdateException();
            }

            return new RefreshTokenResponse
            {
                RefreshToken = newRefreshToken,
                AccessToken = accessToken,
                RefreshTokenExpiry = refreshTokenExpiry,
            };
        }

        private ClaimsPrincipal GetClaimsFromExpiredAccessToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            };

            var handler = new JwtSecurityTokenHandler();

            var principal = handler.ValidateToken(
                token: token,
                tokenValidationParameters,
                out _);

            return principal;
        }
    }
}
