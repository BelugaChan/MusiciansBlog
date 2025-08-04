using Google.Apis.Auth;
using MediatR;
using MusiciansBlog.API.Infrastructure.Users.Common;

namespace MusiciansBlog.API.Infrastructure.Users.AuthGoogle
{
    public class AuthGoogleHandler : IRequestHandler<AuthGoogleCommand>
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public AuthGoogleHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Handle(AuthGoogleCommand request, CancellationToken cancellationToken)
        {
            
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
