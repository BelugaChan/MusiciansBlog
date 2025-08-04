using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure.Users.AuthGoogle
{
    public class AuthGoogleHandler : IRequestHandler<AuthGoogleCommand, AuthGoogleResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;
        private readonly IJWTProvider _provider;
        private readonly JwtOptions _options;

        public AuthGoogleHandler(
            IConfiguration configuration, 
            IUsersRepository usersRepository,
            IJWTProvider provider,
            IOptions<JwtOptions> options)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
            _provider = provider;
            _options = options.Value;
        }

        public async Task<AuthGoogleResponse> Handle(AuthGoogleCommand request, CancellationToken cancellationToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                request.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = ["Authentication:Google:ClientId"]
                });

            var existingUser = await _usersRepository.GetByEmailAsync(payload.Email, cancellationToken);

            var refreshToken = _provider.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiryDays);

            var newUserGuid = Guid.Empty;
            string accessToken = string.Empty;
            if (existingUser is null)
            {
                newUserGuid = Guid.NewGuid();
                var model = new UserModel
                {
                    UserId = newUserGuid,
                    AuthType = AuthType.Google,
                    Email = payload.Email,
                    Created = DateTimeOffset.UtcNow,
                    PasswordHash = string.Empty,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = refreshTokenExpiry,
                    Username = payload.Name
                };
                accessToken = _provider.GenerateToken(model);
            }
            else
            {
                accessToken = _provider.GenerateToken(existingUser);
            }

            return new AuthGoogleResponse
            {
                AccessToken = accessToken,
                RefreshTokenExpiry = refreshTokenExpiry,
                RefreshToken = refreshToken,
                UserId = newUserGuid == Guid.Empty ? existingUser!.UserId : newUserGuid,
            };
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(AuthGoogleCommand command)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = [_configuration.GetSection("Authentication:Google:ClientId").Value]
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(command.IdToken, settings);
            return payload;
        }
    }
}
