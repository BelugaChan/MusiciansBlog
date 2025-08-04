using MediatR;

namespace MusiciansBlog.API.Infrastructure.Users.AuthGoogle
{
    public class AuthGoogleCommand : IRequest<AuthGoogleResponse>
    {
        public string? IdToken { get; set; }
    }
}
