using MediatR;

namespace MusiciansBlog.API.Infrastructure.Users.RefreshTokens
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
